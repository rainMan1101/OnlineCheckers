using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Domain.CQRS.Commands;
using Domain.CQRS.Queries;
using Domain.Entities;
using Microsoft.AspNet.SignalR;
using WebService.Threading;


namespace WebService.Hubs
{
    public class GuestHub : Hub
    {
        private CJoinToGameCommand _joinToGameCommand;
        private CLeaveGameCommand _leaveGameCommand;
        private CGetUserQuery _getUserQuery;
        private CGetGuestQuery _getGuestQuery;
        private GetGuestsQuery _getGuestsQuery;
        private GetPlayersQuery _getPlayersQuery;
        private CGetGameQuery _getGameQuery;
        private CGetCheckersQuery _getCheckersQuery;


        public GuestHub(
            CJoinToGameCommand joinToGameCommand,
            CLeaveGameCommand leaveGameCommand,
            CGetGameQuery getGameQuery,
            CGetUserQuery getUserQuery,
            CGetGuestQuery getGuestQuery,
            GetGuestsQuery getGuestsQuery,
            GetPlayersQuery getPlayersQuery,
            CGetCheckersQuery getCheckersQuery)
        {
            _joinToGameCommand = joinToGameCommand;
            _leaveGameCommand = leaveGameCommand;
            _getUserQuery = getUserQuery;
            _getGuestQuery = getGuestQuery;
            _getGuestsQuery = getGuestsQuery;
            _getPlayersQuery = getPlayersQuery;
            _getGameQuery = getGameQuery;
            _getCheckersQuery = getCheckersQuery;
        }

        // GameId
        public void JoinToGame(Guid gameId)
        {
            CUser user = _getUserQuery.Ask(Guid.Parse(Context.ConnectionId));
            CGame game = _getGameQuery.Ask(gameId);
            CGuest guest = new CGuest(user, game);

            _joinToGameCommand.Execute(guest);

            Groups.Add(Context.ConnectionId, guest.Game.Name);
            Clients.Caller.OnGuestCreated(guest);
            Clients.Group(guest.Game.Name).OnJoinToGame(guest);
            RefreshData(guest);
        }


        public void LeaveGame()
        {
            var guest = _getGuestQuery.Ask(Guid.Parse(Context.ConnectionId));
            Groups.Remove(Context.ConnectionId, guest.Game.Name);
            Clients.Group(guest.Game.Name).OnGuestLeftGame(guest.Id);
            _leaveGameCommand.Execute(guest.Id);
        }


        #region OnReconnected & OnDisconnected

        public override Task OnReconnected()
        {
            var guest = _getGuestQuery.Ask(Guid.Parse(Context.ConnectionId));
            RefreshData(guest);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var guest = _getGuestQuery.Ask(Guid.Parse(Context.ConnectionId));  //TryAsk
            Groups.Remove(Context.ConnectionId, guest.Game.Name);
            Clients.Group(guest.Game.Name).OnGuestLeftGame(guest.Id);
            _leaveGameCommand.Execute(guest.Id);

            return base.OnDisconnected(stopCalled);
        }
        
        #endregion

            
        private void RefreshData(CGuest guest)
        {
            Clients.Caller.OnChangeGameState(PlayerHub.GetGameState(guest.Game.Id));

            if (PlayerHub.TryGetActivePlayer(guest.Game.Id, out var activePlayer))
                Clients.Caller.OnActivePlayerSet(activePlayer);

            Clients.Caller.OnGetCheckerList(_getCheckersQuery.Ask(guest.Game));

            Clients.Caller.OnGetGuestList(_getGuestsQuery.Ask(guest.Game)); 

            var players = _getPlayersQuery.Ask(guest.Game); 

            Clients.Caller.OnGetWhiteTeamPlayersList(
                players.Select(player => player.TeamType == ETeamType.White));

            Clients.Caller.OnGetBlackTeamPlayersList(
                players.Select(player => player.TeamType == ETeamType.Black));
        }



    }
}