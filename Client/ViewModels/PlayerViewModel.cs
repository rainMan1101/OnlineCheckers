using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.ValueTypes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OnlineCheckers.Client.Models;
using OnlineCheckers.Client.ViewModels.Commands;
using OnlineCheckers.Client.ClientHubs;

namespace OnlineCheckers.Client.ViewModels
{
    public class CPlayerViewModel : ILeaveable, INotifyPropertyChanged
    {
        private IPlayerHubClient _playerHubClient;

        private IGuestHubClient _guestHubClient;

        private IChatHubClient _chatHubClient;

        private Object _selectedBoard;

        private CGuest _guest;

        private CPlayerModel _playerModel;

        private CWatchablePlayBoardViewModel _watchableBoard;

        private CMovablePlayBoardViewModel _movableBoard;


        public CPlayerViewModel(
            IPlayerHubClient playerHubClient,
            IGuestHubClient guestHubClient,
            IChatHubClient chatHubClient)
        {
            _guestHubClient = guestHubClient;
            _playerHubClient = playerHubClient;
            _chatHubClient = chatHubClient;

            _watchableBoard = new CWatchablePlayBoardViewModel(Checkers);
            _movableBoard = new CMovablePlayBoardViewModel(_playerModel, Checkers);

            InitPlayerFunctionality();
            InitGuestFunctionality();
            InitChatFunctionality();

            SelectedBoard = _watchableBoard;
        }

        private void InitPlayerFunctionality()
        {
            _playerHubClient.OnPlayerCreated +=
                (player) => _guest = player;

            _playerHubClient.OnEndMoving +=
                () => ActivePlayer = null;
        }

        private void InitGuestFunctionality()
        {
            #region Init

            _guestHubClient.OnGuestCreated +=
                (guest) =>
                {
                    _guest = guest;
                    Game = new CGameModel(guest.Game.Name);
                };

            _guestHubClient.OnGetBlackTeamPlayersList +=
                (players) => BlackPlayers = new ObservableCollection<CPlayer>(players);

            _guestHubClient.OnGetWhiteTeamPlayersList +=
                (players) => WhitePlayers = new ObservableCollection<CPlayer>(players);

            _guestHubClient.OnGetGuestList +=
                (guests) => Guests = new ObservableCollection<CGuest>(guests);

            _guestHubClient.OnGetCheckerList +=
                (checkers) => Checkers = new ObservableCollection<CChecker>(checkers);

            #endregion


            #region Game events

            _guestHubClient.OnChangeGameState +=
                (gameState) =>
                {
                    Game.State = gameState;

                    switch (gameState)
                    {
                        case EGameState.BlackTeamdMoveWaiting:
                            {
                                Boolean isNecessaryTeam = (_guest as CPlayer)?.TeamType == ETeamType.Black;
                                Boolean isNecessaryBoard = SelectedBoard is CMovablePlayBoardViewModel;

                                if (isNecessaryTeam && !isNecessaryBoard)
                                    SelectedBoard = _movableBoard;
                                else if (!isNecessaryTeam && isNecessaryBoard)
                                    SelectedBoard = _watchableBoard;
                            }
                            break;

                        case EGameState.WhiteTeamMoveWaiting:
                            {
                                Boolean isNecessaryTeam = (_guest as CPlayer)?.TeamType == ETeamType.White;
                                Boolean isNecessaryBoard = SelectedBoard is CMovablePlayBoardViewModel;

                                if (isNecessaryTeam && !isNecessaryBoard)
                                    SelectedBoard = _movableBoard;
                                else if (!isNecessaryTeam && isNecessaryBoard)
                                    SelectedBoard = _watchableBoard;
                            }
                            break;

                        case EGameState.MoveDoing:
                            if (ActivePlayer.Id != _guest.Id && !(SelectedBoard is CWatchablePlayBoardViewModel))
                                SelectedBoard = _watchableBoard;
                            break;

                        case EGameState.Freeze:
                            if (!(SelectedBoard is CWatchablePlayBoardViewModel))
                                SelectedBoard = _watchableBoard;
                            break;

                        case EGameState.Close:
                            OnLeave();
                            break;
                    }
                };

            _guestHubClient.OnActivePlayerSet +=
                (player) => ActivePlayer = player;


            _guestHubClient.OnMoveChecker +=
                (checker) => Checkers[checker.Id - 1] = checker;

            #endregion


            #region Guest && Game

            _guestHubClient.OnGuestLeftGame +=
                (guestId) => Guests.Remove(guest => guest.Id == guestId);

            _guestHubClient.OnJoinToGame +=
                (guest) => Guests.Add(guest);

            #endregion


            #region Player && Team

            _guestHubClient.OnJoinToTeam +=
                (player) =>
                {
                    switch (player.TeamType)
                    {
                        case ETeamType.White: WhitePlayers.Add(player); break;
                        case ETeamType.Black: BlackPlayers.Add(player); break;
                        default: throw new Exception("Unknown player team type");
                    }
                };

            _guestHubClient.OnLeaveTeam +=
                (playerId, teamType) =>
                {
                    switch (teamType)
                    {
                        case ETeamType.White: WhitePlayers.Remove(player => player.Id == playerId); break;
                        case ETeamType.Black: BlackPlayers.Remove(player => player.Id == playerId); break;
                        default: throw new Exception("Unknown player team type");
                    }
                };

            _guestHubClient.OnChangeTeam +=
                (changedPlayer) =>
                {
                    switch (changedPlayer.TeamType)
                    {
                        case ETeamType.White:
                            WhitePlayers.Add(changedPlayer);
                            BlackPlayers.Remove(player => player.Id == changedPlayer.Id);
                            break;

                        case ETeamType.Black:
                            BlackPlayers.Add(changedPlayer);
                            WhitePlayers.Remove(player => player.Id == changedPlayer.Id);
                            break;
                        default: throw new Exception("Unknown player team type");
                    }
                };

            #endregion

        }

        private void InitChatFunctionality()
        {
            _chatHubClient.OnGetMessageList +=
                (messages) => Messages = new ObservableCollection<CMessage>(messages);

            _chatHubClient.OnSendMessage +=
                (message) => Messages.Add(message);
        }



        #region Binding properties

        public Object SelectedBoard
        {

            get => _selectedBoard;

            private set
            {
                _selectedBoard = value;
                OnPropertyChanged();
            }
        }

        public CGameModel Game { get; set; }

        public CPlayer ActivePlayer { get; set; }

        public String MessageText { get; set; }

        public ObservableCollection<CPlayer> WhitePlayers { get; private set; }

        public ObservableCollection<CPlayer> BlackPlayers { get; private set; }

        public ObservableCollection<CGuest> Guests { get; private set; }

        public ObservableCollection<CChecker> Checkers { get; private set; }

        public ObservableCollection<CMessage> Messages { get; private set; }

        #endregion


        #region Commands

        public CRelayCommand SendMessage
        {
            get
            {
                return _sendMessage ??
                       (_sendMessage = new CRelayCommand(obj =>
                       {
                           if (String.IsNullOrEmpty(MessageText) && String.IsNullOrWhiteSpace(MessageText))
                           {
                               _chatHubClient.SendMessage(MessageText);
                               MessageText = "";
                           }

                       }));
            }
        }

        public CRelayCommand JoinToBlackTeam
        {
            get
            {
                return _joinToBlackTeam ??
                       (_joinToBlackTeam = new CRelayCommand(obj =>
                       {
                           //TODO: Migrate method to guestHub
                           _playerHubClient.JoinToTeam(ETeamType.Black);
                       }));
            }
        }

        public CRelayCommand JoinToWhiteTeam
        {
            get
            {
                return _joinToWhiteTeam ??
                       (_joinToWhiteTeam = new CRelayCommand(obj =>
                       {
                           //TODO: Migrate method to guestHub
                           _playerHubClient.JoinToTeam(ETeamType.White);
                       }));
            }
        }


        private CRelayCommand _sendMessage;
        private CRelayCommand _joinToWhiteTeam;
        private CRelayCommand _joinToBlackTeam;

        #endregion


        public event EventHandler Leave;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OnLeave()
        {
            Leave?.Invoke(this, new EventArgs());
        }

    }
}