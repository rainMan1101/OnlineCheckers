using Domain.Services.UnitOfWork;
using Domain.Services.Repositories;
using OnlineCheckers.DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage.UnitOfWork
{
    public class COnlineCheckersUnitOfWork : CUnitOfWork, IOnlineCheckersUnitOfWork
    {
        public COnlineCheckersUnitOfWork(IDbConnection connection) : base(connection)
        {
        }

        public ICheckerRepository GetCheckerRepository()
        {
            return new CCheckerRepository(Transaction);
        }

        public IGameRepository GetGameRepository()
        {
            return new CGameRepository(Transaction);
        }

        public IGuestRepository GetGuestRepository()
        {
            return new CGuestRepository(Transaction);
        }

        public IMessageRepository GetMessageRepository()
        {
            return new CMessageRepository(Transaction);
        }

        public IChangeRepository GetChangeRepository()
        {
            return new CChangeRepository(Transaction);
        }

        public IPlayerRepository GetPlayerRepository()
        {
            return new CPlayerRepository(Transaction);
        }

        public IUserRepository GetUserRepository()
        {
            return new CUserRepository(Transaction);
        }
    }
}
