using Domain.Entities;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs.Implementation
{
    public class CGuestHubClient : IGuestHubClient
    {
        public event Action<CGuest> OnJoinToGame;
        public event Action<Guid> OnGuestLeftGame;
        public event Action<IEnumerable<CGuest>> OnGetGuestList;
        public event Action<IEnumerable<CPlayer>> OnGetWhiteTeamPlayersList;
        public event Action<IEnumerable<CPlayer>> OnGetBlackTeamPlayersList;
        public event Action<CPlayer> OnJoinToTeam;
        public event Action<Guid,ETeamType> OnLeaveTeam;
        public event Action<EGameState> OnChangeGameState;      // !!!
        public event Action<CPlayer> OnActivePlayerSet;
        public event Action<CChecker> OnMoveChecker;
        public event Action<CGuest> OnGuestCreated;
        public event Action<CPlayer> OnChangeTeam;
        public event Action<IEnumerable<CChecker>> OnGetCheckerList;

        private IHubProxy _guestHubProxy;

        public CGuestHubClient(IHubProxy guestHubProxy)
        {
            _guestHubProxy = guestHubProxy;

            _guestHubProxy.On<CGuest>("OnJoinToGame",
                (guest) => OnJoinToGame.Invoke(guest));
            _guestHubProxy.On<Guid>("OnGuestLeftGame",
                (guestId) => OnGuestLeftGame.Invoke(guestId));

            _guestHubProxy.On<IEnumerable<CGuest>>("OnGetGuestList",
                (guests) => OnGetGuestList.Invoke(guests));
            _guestHubProxy.On<IEnumerable<CPlayer>>("OnGetWhiteTeamPlayersList",
                (players) => OnGetWhiteTeamPlayersList.Invoke(players));
            _guestHubProxy.On<IEnumerable<CPlayer>>("OnGetBlackTeamPlayersList",
                (players) => OnGetBlackTeamPlayersList.Invoke(players));
            _guestHubProxy.On<IEnumerable<CChecker>>("OnGetCheckerList",
                (checkers) =>OnGetCheckerList.Invoke(checkers));

            _guestHubProxy.On<CPlayer>("OnJoinToTeam",
                (player) => OnJoinToTeam.Invoke(player));
            _guestHubProxy.On<Guid, ETeamType>("OnLeaveTeam",
                (playerId, teamType) => OnLeaveTeam.Invoke(playerId, teamType));
            _guestHubProxy.On<EGameState>("OnChangeGameState",
                (gameState) => OnChangeGameState.Invoke(gameState));
            _guestHubProxy.On<CPlayer>("OnActivePlayerSet",
                (player) => OnActivePlayerSet.Invoke(player));
            _guestHubProxy.On<CChecker>("OnMoveChecker",
                (checker) => OnMoveChecker.Invoke(checker));
            _guestHubProxy.On<CGuest>("OnGuestCreated",
                (guest) => OnGuestCreated.Invoke(guest));
            _guestHubProxy.On<CPlayer>("OnChangeTeam",
                (player) => OnChangeTeam.Invoke(player));
        }



        public void JoinToGame(Guid gameId)
        {
            _guestHubProxy.Invoke("JoinToGame", gameId);
        }

        public void LeaveGame()
        {
            _guestHubProxy.Invoke("LeaveGame");
        }
    }
}
