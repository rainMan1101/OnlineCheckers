using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Domain.Entities;
using Domain.CQRS.Commands;
using Domain.CQRS.Queries;
using System;

namespace WebService.Hubs
{
    public class UserHub : Hub
    {
        private CreateUserCommand _createUserCommand;
        private DropUserCommand _dropUserCommand;
        private CGetGamesQuery _getGamesQuery;


        public UserHub(
            CreateUserCommand createUserCommand,
            DropUserCommand dropUserCommand,
            CGetGamesQuery getGamesQuery)
        {
            //TODO: Guard
            _createUserCommand = createUserCommand;
            _dropUserCommand = dropUserCommand;
            _getGamesQuery = getGamesQuery;
        }

        public void CreateUser(String name)
        {
            var user = new CUser(Guid.NewGuid(), name);
            _createUserCommand.Execute(user); 
            Clients.Caller.OnUserCreated(user);
            
            RefreshData();
        }


        #region OnReconnected & OnDisconnected

        public override Task OnReconnected()
        {
            RefreshData();
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            _dropUserCommand.Execute(Guid.Parse(Context.ConnectionId));

            return base.OnDisconnected(stopCalled);
        }

        #endregion

        private void RefreshData()
        {
            var games = _getGamesQuery.Ask();
            Clients.Caller.OnGetGamesList(games); // Last 20?
        }
    }
}