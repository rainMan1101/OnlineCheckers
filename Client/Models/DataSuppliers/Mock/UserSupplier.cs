using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models.DataSuppliers.Mock
{
    public class CUserSupplier : IUserSupplier
    {
        Guid _userId;

        public CUserSupplier(Guid userId)
        {
            _userId = userId;
        }

        public void JoinToGame(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public List<CGameInfo> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public CUser GetUserInfo()
        {
            throw new NotImplementedException();
        }

        // Guid - Id game
        public Guid CreateNewGame(string gameName, ETeamType teamType)
        {
            throw new NotImplementedException();
        }
    }
}
