using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using OnlineCheckers.Client.ViewModels.Commands;
using OnlineCheckers.Client.Models.DataSuppliers;
using System.Windows;
using OnlineCheckers.Client.ClientHubs;

namespace OnlineCheckers.Client.ViewModels
{
    public class CAddGameViewModel : ILeaveable
    {
        public String GameName { get; set; } // Binding?

        private ETeamType TeamType { get; set; } = ETeamType.White;

        // TODO: converter
        public Boolean IsBlackChecked
        {
            get { return _isBlackChecked; }
            set {
                _isBlackChecked = value;

                if (value) TeamType = ETeamType.Black;
                else TeamType = ETeamType.White;

            }
        }

        public Boolean _isBlackChecked;

        private IPlayerHubClient _playerHubClient;


        public CAddGameViewModel(IPlayerHubClient playerHubClient)
        {
            _playerHubClient = playerHubClient;
        }


        #region Commands

        public CRelayCommand CreateGameCommand =>
            _createGameCommand ?? (_createGameCommand = new CRelayCommand(
                o => {

                    try
                    {
                        MessageBox.Show(GameName);
                        _playerHubClient.CreateGame(GameName, TeamType);
                        OnGameCreate();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }, 
                o => !String.IsNullOrWhiteSpace(GameName) && !String.IsNullOrEmpty(GameName)
                ));


        public CRelayCommand ClosePageCommand =>
            _closePageCommand ?? (_closePageCommand = new CRelayCommand(o => OnLeave()));

        private CRelayCommand _createGameCommand;

        private CRelayCommand _closePageCommand;

        #endregion



        public event EventHandler GameCreate;

        public event EventHandler Leave;

        private void OnGameCreate() => GameCreate?.Invoke(this, new EventArgs());

        private void OnLeave() => Leave?.Invoke(this, new EventArgs());
    }
}
