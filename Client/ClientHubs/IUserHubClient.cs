using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs
{
    public interface IUserHubClient
    {
        event Action<CUser> OnUserCreated;
        event Action<IEnumerable<CGame>> OnGetGamesList;
        event Action<CGame> OnGameCreated;
        event Action<Guid> OnGameDroped;


        void CreateUser(String name);
    }
}
