using Domain.Entities;
using Domain.ValueTypes;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs.Implementation
{
    public class CPlayerHubClient : IPlayerHubClient
    {
        public event Action<DateTime> OnPlayerActive;
        public event Action OnEndMoving;
        public event Action<CPlayer> OnPlayerCreated;


        private IHubProxy _playerHubProxy;


        public CPlayerHubClient(IHubProxy playerHubProxy)
        {
            _playerHubProxy = playerHubProxy;

            _playerHubProxy.On<DateTime>("OnPlayerActive",
                (date) => OnPlayerActive.Invoke(date));
            _playerHubProxy.On("OnEndMoving",
                () => OnEndMoving.Invoke());
            _playerHubProxy.On<CPlayer>("OnPlayerCreated",
                (player) => OnPlayerCreated.Invoke(player));
        }


        public void CreateGame(String gameName, ETeamType teamType)
        {
            _playerHubProxy.Invoke("CreateGame", gameName, teamType);
        }

        public void JoinToTeam(ETeamType teamType)
        {
            _playerHubProxy.Invoke("JoinToTeam", teamType);
        }

        public void ChangeTeam(ETeamType teamType)
        {
            _playerHubProxy.Invoke("ChangeTeam", teamType);
        }

        public void LeaveTeam()
        {
            _playerHubProxy.Invoke("LeaveTeam");
        }

        public void BeActive(CChecker checker)
        {
            _playerHubProxy.Invoke("BeActive", checker);
        }

        public void Move(CLocation newLocation)
        {
            _playerHubProxy.Invoke("Move", newLocation);
        }

        public void LoseMoving()
        {
            _playerHubProxy.Invoke("LoseMoving");
        }
    }
}
