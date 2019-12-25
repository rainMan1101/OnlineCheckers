using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.CQRS.Commands;
using Domain.CQRS.Contexts;
using Domain.CQRS.Queries;
using Domain.Entities;
using Domain.ValueTypes;
using Microsoft.AspNet.SignalR;
using WebService.Threading;

namespace WebService.Hubs
{
    public class PlayerHub : Hub
    {
        private CGetGuestQuery _getGuestQuery;
        private CGetPlayerQuery _getPlayerQuery;
        private CGetUserQuery _getUserQuery;
        private CJoinToTeamCommand _joinToTeamCommand;
        private CLeaveTeamCommand _leaveTeamCommand;
        private CMoveCommand _moveCommand;
        private CChangeTeamCommand _changeTeamCommand;
        private CCreateGameCommand _createGameCommand;
        private CDropGameCommand _dropGameCommand;


        private readonly static CSynchronizedCache<Guid, CGameActivity> _activePlayersAndCheckers
            = new CSynchronizedCache<Guid, CGameActivity>(); // gameId / activePlayerId & CChecker

        private readonly static CSynchronizedCache<Guid, EGameState> _gamesStates
            = new CSynchronizedCache<Guid, EGameState>();

        private readonly static CSynchronizedCache<Guid, Object> _syncMove
            = new CSynchronizedCache<Guid, Object>();

        private readonly static CSynchronizedCache<Guid, Object> _syncBeActive
            = new CSynchronizedCache<Guid, Object>();

        public readonly static CSynchronizedCache<Guid, Int32> _moveCounters
            = new CSynchronizedCache<Guid, Int32>();



        public PlayerHub(
            CGetGuestQuery getGuestQuery,
            CGetPlayerQuery getPlayerQuery,
            CGetUserQuery getUserQuery, 
            CMoveCommand moveCommand,
            CJoinToTeamCommand joinToTeamCommand,
            CLeaveTeamCommand leaveTeamCommand,
            CChangeTeamCommand changeTeamCommand,
            CCreateGameCommand createGameCommand,
            CDropGameCommand dropGameCommand)
        {
            _joinToTeamCommand = joinToTeamCommand;
            _moveCommand = moveCommand;
            _getPlayerQuery = getPlayerQuery;
            _getGuestQuery = getGuestQuery;
            _leaveTeamCommand = leaveTeamCommand;
            _changeTeamCommand = changeTeamCommand;
            _createGameCommand = createGameCommand;
            _dropGameCommand = dropGameCommand;
            _getUserQuery = getUserQuery; 
        }


        public static EGameState GetGameState(Guid gameId)
        {
            return _gamesStates.GetValue(gameId);
        }

        public static Boolean TryGetActivePlayer(Guid gameId, out CPlayer player)
        {
            Boolean result = _activePlayersAndCheckers.TryGetValue(gameId, out var activity);
            player = activity?.Player;

            return result;
        }


        public void CreateGame(String gameName, ETeamType teamType)
        {
            var user = _getUserQuery.Ask(Guid.Parse(Context.ConnectionId));
            var game = new CGame(gameName);
            var player = new CPlayer(new CGuest(user, game), teamType);

            // Отложить exception
            Clients.Caller.OnPlayerCreated(player);
            UserHubCall().OnGameCreated(game);

            _createGameCommand.Execute(player);
            _gamesStates.Add(game.Id, game.State);
            _syncMove.Add(game.Id, new Object());
            _syncBeActive.Add(game.Id, new Object());
            _moveCounters.Add(game.Id, 0);
            ChatHub.AddGametInfo(game);
        }
        
        public void JoinToTeam(ETeamType teamType)
        {
            CGuest guest = _getGuestQuery.Ask(Guid.Parse(Context.ConnectionId));
            var player = new CPlayer(guest, teamType);
            EGameState oldGameState = player.Game.State;

            _joinToTeamCommand.Execute(player); //!!!
            Clients.Caller.OnPlayerCreated(player);
            GuestHubCall(player.Game.Name).OnJoinToTeam(player);

            if (oldGameState != player.Game.State)
            {
                _gamesStates.SetValue(player.Game.Id, player.Game.State);
                GuestHubCall(player.Game.Name).OnChangeGameState(player.Game.State);
            }
        }

        public void ChangeTeam(ETeamType teamType)
        {
            CPlayer player = _getPlayerQuery.Ask(Guid.Parse(Context.ConnectionId));
            EGameState oldGameState = player.Game.State;
            player.ChangeTeam(teamType);

            _changeTeamCommand.Execute(player);
            GuestHubCall(player.Game.Name).OnChangeTeam(player);

            if (oldGameState != player.Game.State)
            {
                _gamesStates.SetValue(player.Game.Id, player.Game.State);
                GuestHubCall(player.Game.Name).OnChangeGameState(player.Game.State);
            }
        }

        public void LeaveTeam()
        {
            CPlayer player = _getPlayerQuery.Ask(Guid.Parse(Context.ConnectionId));

            _leaveTeamCommand.Execute(player); //!!!
            GuestHubCall(player.Game.Name).OnLeaveTeam(player.Id, player.TeamType);

            if (player.Game.State == EGameState.Close || player.Game.State == EGameState.Freeze)
            {
                GuestHubCall(player.Game.Name).OnChangeGameState(player.Game.State);

                if (player.Game.State == EGameState.Close)
                {
                    UserHubCall().OnGameDroped(player.Game.Id);
                    ChatHub.DeleteGameInfo(player.Game.Id);
                    _gamesStates.Delete(player.Game.Id);
                    _dropGameCommand.Execute(player.Game);

                    _gamesStates.Delete(player.Game.Id);
                    _syncMove.Delete(player.Game.Id);
                    _syncBeActive.Delete(player.Game.Id);
                    _moveCounters.Delete(player.Game.Id);
                }
                else
                    _gamesStates.SetValue(player.Game.Id, player.Game.State);
            }
           
        }

        public void BeActive(CChecker checker)
        {
            var player = _getPlayerQuery.Ask(Guid.Parse(Context.ConnectionId));

            if (checker.Game.Id == player.Game.Id)
            {
                if (CanBeActive(player.Game.Id, player.TeamType))
                {
                    if (_syncBeActive.TryGetValue(player.Game.Id, out var syncObj))
                    {
                        lock (syncObj)
                        {
                            if (CanBeActive(player.Game.Id, player.TeamType))
                            {
                                _gamesStates.SetValue(player.Game.Id, EGameState.MoveDoing);
                                _activePlayersAndCheckers.Add(player.Game.Id, new CGameActivity(player, checker)); //!!!

                                Task.Run(async () =>
                                {
                                    await Task.Delay(10000);
                                    EndMovingWithLock(player);
                                });

                                // TODO: sync time
                                Clients.Caller.OnPlayerActive(DateTime.Now.AddSeconds(10));
                                GuestHubCall(player.Game.Name).OnActivePlayerSet(player, DateTime.Now.AddSeconds(10)); // можно отправить и захваченную шашку
                                GuestHubCall(player.Game.Name).OnChangeGameState(player.Game.State);
                            }
                        }
                    }
                }

                
            }

        }


        public void LoseMoving()
        {
            var player = _getPlayerQuery.Ask(Guid.Parse(Context.ConnectionId));
            EndMovingWithLock(player);
        }


        public void Move(CLocation newLocation)
        {
            var player = _getPlayerQuery.Ask(Guid.Parse(Context.ConnectionId));

            if (CheckActive(player, out var activity))
            {
                if (_syncMove.TryGetValue(player.Game.Id, out var syncObj))
                {
                    lock (syncObj)
                    {
                        if (CheckActive(player, out activity))
                        {
                            Int32 counter = _moveCounters.GetValue(player.Game.Id);
                            activity.Checker.Move(newLocation);

                            _moveCommand.Execute(new CChange(++counter, player, activity.Checker));
                            GuestHubCall(player.Game.Name).OnMoveChecker(activity.Checker);

                            EndMoving(player);
                        }
                    }
                }
            }
        }



        private void EndMovingWithLock(CPlayer player)
        {
            if (CheckActive(player, out var activity))
            {
                if (_syncMove.TryGetValue(player.Game.Id, out var syncObj))
                {
                    lock (syncObj)
                    {
                        if (CheckActive(player, out activity))
                        {
                            EndMoving(player);
                        }
                    }
                }
            }
        }

        private void EndMoving(CPlayer player)
        {
            Clients.Caller.OnEndMoving();
            _activePlayersAndCheckers.Delete(player.Game.Id);

            EGameState newGameState = GetNextMovingTeamState(player.TeamType);
            _gamesStates.SetValue(player.Game.Id, newGameState);
            GuestHubCall(player.Game.Name).OnChangeGameState(newGameState);
        }


        private Boolean CheckActive(CPlayer player, out CGameActivity activity)
        {
            if (_gamesStates.TryGetValue(player.Game.Id, out var state) && state == EGameState.MoveDoing)
            {
                return _activePlayersAndCheckers.TryGetValue(player.Game.Id, out activity) && activity.Player.Id == player.Id;
            }
            else
            {
                activity = default(CGameActivity);
                return false;
            }
        }


        private EGameState GetNextMovingTeamState(ETeamType teamType)
        {
            switch (teamType)
            {
                case ETeamType.Black: return EGameState.WhiteTeamMoveWaiting;
                case ETeamType.White: return EGameState.BlackTeamdMoveWaiting;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private dynamic ChatHubCall(String groupName)
        {
            return GlobalHost.ConnectionManager.GetHubContext<ChatHub>().Clients.Group(groupName);
        }

        private dynamic GuestHubCall(String groupName)
        {
            return GlobalHost.ConnectionManager.GetHubContext<GuestHub>().Clients.Group(groupName);
        }

        private dynamic UserHubCall()
        {
            return GlobalHost.ConnectionManager.GetHubContext<UserHub>().Clients;
        }

        private Boolean CanBeActive(Guid gameId, ETeamType playerTeamType)
        {
            EGameState gameState = _gamesStates.GetValue(gameId);

            return (gameState != EGameState.MoveDoing)
                ? CanBeAvtive(gameState, playerTeamType)
                : false;
        }

        private Boolean CanBeAvtive(EGameState gameState, ETeamType teamType)
        {
            return
                (gameState == EGameState.BlackTeamdMoveWaiting && teamType == ETeamType.Black)
                || (gameState == EGameState.WhiteTeamMoveWaiting && teamType == ETeamType.White);
        }

    }
}