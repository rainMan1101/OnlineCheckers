using System;
using System.Windows;
using OnlineCheckers.Client.ViewModels.Commands;
using OnlineCheckers.Client.Models.DataSuppliers;
using OnlineCheckers.Client.Models.DataSuppliers.Implements;
using OnlineCheckers.Client.ClientHubs;

namespace OnlineCheckers.Client.ViewModels
{
    public class CSignInViewModel
    {

        public String UserName { get; set; }

        private IUserHubClient _userHubClient;


        public CSignInViewModel(IUserHubClient userHubClient)
        {
            _userHubClient = userHubClient;
        }


        #region Command

        public CRelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new CRelayCommand(
                    o => {
                        try
                        {
                            _userHubClient.CreateUser(UserName);
                            OnAuthorized();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                ));
            }
        }

        private CRelayCommand _signInCommand;

        #endregion


        public event Action Authorized;

        private void OnAuthorized()
        {
            Authorized?.Invoke();
        }
    }
}
