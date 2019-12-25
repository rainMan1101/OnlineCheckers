using Domain.Entities;
using Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs
{
    public interface IPlayerHubClient
    {
        event Action<CPlayer> OnPlayerCreated;
        event Action<DateTime> OnPlayerActive; //!!!!!!!!
        event Action OnEndMoving;//!!!!!!!!


        void CreateGame(String gameName, ETeamType teamType);
        void JoinToTeam(ETeamType teamType);
        void ChangeTeam(ETeamType teamType);
        void LeaveTeam();
        void BeActive(CChecker checker);//!!!!!!!!
        void Move(CLocation newLocation);//!!!!!!!!
        void LoseMoving();//!!!!!!!!
    }
}
