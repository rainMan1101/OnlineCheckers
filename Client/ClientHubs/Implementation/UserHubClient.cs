using Domain.Entities;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs.Implementation
{
    public class CUserHubClient : IUserHubClient
    {
        public event Action<CUser> OnUserCreated;
        public event Action<IEnumerable<CGame>> OnGetGamesList;
        public event Action<CGame> OnGameCreated;
        public event Action<Guid> OnGameDroped;

        private IHubProxy _userHubProxy;
        

        public CUserHubClient(IHubProxy userHubProxy)
        {
            _userHubProxy = userHubProxy;

            _userHubProxy.On<CUser>("OnUserCreated", 
                (user) => OnUserCreated.Invoke(user));
            _userHubProxy.On<IEnumerable<CGame>>("OnGetGamesList", 
                (games) => OnGetGamesList.Invoke(games));
            _userHubProxy.On<CGame>("OnGameCreated",
                (game) => OnGameCreated.Invoke(game));
            _userHubProxy.On<Guid>("OnGameDroped",
                (gameId) => OnGameDroped.Invoke(gameId));
        }

        public void CreateUser(String name)
        {
            _userHubProxy.Invoke("CreateUser", name);
        }
    }
}
