using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OnlineCheckers.Client.ClientHubs;
using OnlineCheckers.Client.Models;

namespace OnlineCheckers.Client.ViewModels
{
    public class CMainViewModel : INotifyPropertyChanged
    {
        private IUserHubClient _userHubClient;

        private CUserViewModel _userViewModel;

        private object _selectedGloobalViewModel;


        public object SelectedGlobalViewModel
        {
            get => _selectedGloobalViewModel; 
            set
            {
                _selectedGloobalViewModel = value;
                OnPropertyChanged();
            }
        }


        public CMainViewModel(IUserHubClient userHubClient)
        {
            _userHubClient = userHubClient;

            _userViewModel = new CUserViewModel(_userHubClient);

            var signInViewModel = new CSignInViewModel(_userHubClient);
            signInViewModel.Authorized += 
                () => SelectedGlobalViewModel = _userViewModel;
            SelectedGlobalViewModel = signInViewModel;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
