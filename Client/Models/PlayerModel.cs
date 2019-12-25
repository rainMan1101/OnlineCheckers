using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueTypes;
using OnlineCheckers.Client.ClientHubs;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models
{
    public class CPlayerModel
    {
        private IPlayerHubClient _playerHubClient;

        public CPlayerModel(IPlayerHubClient playerHubClient)
        {
            _playerHubClient = playerHubClient;

            _playerHubClient.OnPlayerActive +=
                (dateTime) => IsActive = true;

            _playerHubClient.OnEndMoving +=
                () => IsActive = false;
        }

        public Boolean IsActive { get; private set; }


        public void BeActive(CChecker checker)
        {
            _playerHubClient.BeActive(checker);
        }

        public void Move(CLocation newLocation)
        {
            _playerHubClient.Move(newLocation);
        }

        public void LoseMoving()
        {
            _playerHubClient.LoseMoving();
        }
    }
}
