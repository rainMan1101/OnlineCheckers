using Microsoft.AspNet.SignalR;
using Domain.Entities;
using Domain.ValueTypes;
using WebService.Threading;
using System;
using System.Threading.Tasks;
using Domain.CQRS.Queries;
using Domain.CQRS.Contexts;
using System.Collections.Generic;
using Domain.CQRS.Commands;

namespace WebService.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static CSynchronizedCache<String, Guid> _chatUsers
            = new CSynchronizedCache<String, Guid>();

        private readonly static CSynchronizedCache<Guid, CGameInfo> _gameInfo
            = new CSynchronizedCache<Guid, CGameInfo>(); // устанавливается вместе с созанием игры


        private CGetMessagesQuery _getMessagesQuery;
        private CGetGuestQuery _getGuestQuery;
        private CSendMessageCommand _sendMessageCommand;

        public ChatHub(
            CGetMessagesQuery getMessagesQuery,
            CGetGuestQuery getGuestQuery,
            CSendMessageCommand sendMessageCommand)
        {
            _getMessagesQuery = getMessagesQuery;
            _getGuestQuery = getGuestQuery;
            _sendMessageCommand = sendMessageCommand;
        }


        public static void AddGametInfo(CGame game)
        {
            _gameInfo.Add(game.Id, new CGameInfo(new CGame(game.Name)));
        }

        public static void DeleteGameInfo(Guid gameId)
        {
            _gameInfo.Delete(gameId);
        }

        public void JoinToChat(Guid gameId)
        {
            if (!_gameInfo.TryGetValue(gameId, out var gameInfo))
            {
                Clients.Caller.OnGameClosed();
                return;
            }

            if (!_chatUsers.TryAdd(Context.ConnectionId, gameId))
            {
                Clients.Caller.OnUserExistsYet();
                return;
            }

            Groups.Add(Context.ConnectionId, gameInfo.Game.Name);

            Clients.Caller.OnGetMessageList(
                    _getMessagesQuery.Ask(
                        new CGetMessageContext(
                            gameInfo.Game,
                            gameInfo.LastMessageIndex - 20,
                            gameInfo.LastMessageIndex))
                );
        }

        public void LeaveChat()
        {
            if(!_chatUsers.TryGetValue(Context.ConnectionId, out var gameId))
            {
                Clients.Caller.OnUserDoesNotExists();
                return;
            }

            if (!_gameInfo.TryGetValue(gameId, out var gameInfo))
            {
                Clients.Caller.OnGameClosed();
                return;
            }

            _chatUsers.Delete(Context.ConnectionId);
            Groups.Remove(Context.ConnectionId, gameInfo.Game.Name);
        }


        public void SendMessage(String messageText)
        {
            if (!_chatUsers.TryGetValue(Context.ConnectionId, out var gameId))
            {
                Clients.Caller.OnUserDoesNotExists();
                return;
            }

            if (!_gameInfo.TryGetValue(gameId, out var gameInfo))
            {
                Clients.Caller.OnGameClosed();
                return;
            }

            CGuest guest = _getGuestQuery.Ask(Guid.Parse(Context.ConnectionId));
            CMessage message = new CMessage(++gameInfo.LastMessageIndex, guest, messageText);

            _sendMessageCommand.Execute(message);
            Clients.Group(gameInfo.Game.Name).OnSendMessage(message);
        }


        public void GetMessages(Int32 startIndex, Int32 endIndex)
        {
            if (!_chatUsers.TryGetValue(Context.ConnectionId, out var gameId))
            {
                Clients.Caller.OnUserDoesNotExists();
                return;
            }

            if (!_gameInfo.TryGetValue(gameId, out var gameInfo))
            {
                Clients.Caller.OnGameClosed();
                return;
            }

            Clients.Caller.OnGetMessageList(
                        _getMessagesQuery.Ask(new CGetMessageContext(gameInfo.Game, startIndex, endIndex))
                    );
        }


        #region OnReconnected & OnDisconnected

        public override Task OnDisconnected(bool stopCalled)
        {
            _chatUsers.Delete(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            if (_chatUsers.TryGetValue(Context.ConnectionId, out var gameId) &&
                _gameInfo.TryGetValue(gameId, out var gameInfo))
            {
                Clients.Caller.OnGetMessageList(
                        _getMessagesQuery.Ask(
                            new CGetMessageContext(
                                gameInfo.Game,
                                gameInfo.LastMessageIndex - 20,
                                gameInfo.LastMessageIndex))
                    );
            }

            return base.OnReconnected();
        }

        #endregion

    }
}