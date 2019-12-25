using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs
{
    public interface IGuestHubClient
    {
        // Guests
        event Action<CGuest> OnGuestCreated;
        event Action<CGuest> OnJoinToGame;
        event Action<Guid> OnGuestLeftGame;
        event Action<IEnumerable<CGuest>> OnGetGuestList;

        // Players
        event Action<IEnumerable<CPlayer>> OnGetWhiteTeamPlayersList;
        event Action<IEnumerable<CPlayer>> OnGetBlackTeamPlayersList;
        event Action<CPlayer> OnJoinToTeam; // My user?
        event Action<Guid, ETeamType> OnLeaveTeam;
        event Action<CPlayer> OnChangeTeam;

        // Game
        event Action<EGameState> OnChangeGameState;     
        event Action<CPlayer> OnActivePlayerSet;
        event Action<CChecker> OnMoveChecker;
        event Action<IEnumerable<CChecker>> OnGetCheckerList;

        void JoinToGame(Guid gameId);
        void LeaveGame();
    }
}
