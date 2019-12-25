using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using OnlineCheckers.Client.ViewModels.Commands;
using OnlineCheckers.Client.Models.DataSuppliers;
using OnlineCheckers.Client.Models.DataSuppliers.Implements;
using OnlineCheckers.Client.ClientHubs;
using OnlineCheckers.Client.ClientHubs.Implementation;
using Domain.Entities;
using OnlineCheckers.Client.Views;
using OnlineCheckers.Client.Models;

namespace OnlineCheckers.Client.ViewModels
{
    public class CUserViewModel : INotifyPropertyChanged
    {
        private IUserHubClient _userHubClient;

        private IPlayerHubClient _playerHubClient;

        private IGuestHubClient _guestHubClient;

        private CPlayerViewModel _playerViewModel;

        private CAddGameViewModel _addGameViewModel;


        public CUserViewModel(IUserHubClient userHubClient)
        {
            _userHubClient = userHubClient;

            _userHubClient.OnUserCreated +=
                (user) => User = new CUserModel(user.Id, user.Name);

            _userHubClient.OnGetGamesList +=
                (games) => Games = new ObservableCollection<CGame>(games);

            _userHubClient.OnGameCreated +=
                (game) => Games.Add(game);

            _userHubClient.OnGameDroped +=
                (gameId) => Games.Remove(game => game.Id == gameId);
            

            _playerHubClient = new CPlayerHubClient(App.PlayerHubProxy);
            _guestHubClient = new CGuestHubClient(App.GuestHubProxy);
            var chatHubClient = new CChatHubClient(App.ChatHubProxy);

            _addGameViewModel = new CAddGameViewModel(_playerHubClient);
            _playerViewModel = new CPlayerViewModel(_playerHubClient, _guestHubClient, chatHubClient);
        }


        #region Properties

        public CUserModel User
        {
            get => _user;
            private set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        private CUserModel _user;

        public ObservableCollection<CGame> Games { get; private set; }

        public CGame SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                _guestHubClient.JoinToGame(_selectedGame.Id);
                SelectedViewModel = _playerViewModel;
                SelectedViewModel.Leave += (o, e) => SelectedViewModel = null;
            }
        }

        private CGame _selectedGame;


        public ILeaveable SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        private ILeaveable _selectedViewModel;

        #endregion


        #region Commands

        public CRelayCommand CreateGameCommand
        {
            get
            {
                return _createNewGame ??
                    (_createNewGame = new CRelayCommand(obj => {

                        _addGameViewModel.GameCreate +=
                            (o, e) => SelectedViewModel = _playerViewModel;
                        _addGameViewModel.Leave +=
                            (o, e) => SelectedViewModel = null;

                        SelectedViewModel = _addGameViewModel;
                        }));
            }
        }

        private CRelayCommand _createNewGame;
        
        #endregion


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        
        #endregion
    }
}
