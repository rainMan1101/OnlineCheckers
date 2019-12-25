using Domain.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.UnitOfWork
{
    public interface IOnlineCheckersUnitOfWork : IUnitOfWork, IDisposable
    {
        ICheckerRepository GetCheckerRepository();

        IGameRepository GetGameRepository();

        IGuestRepository GetGuestRepository();

        IMessageRepository GetMessageRepository();

        IChangeRepository GetChangeRepository();

        IPlayerRepository GetPlayerRepository();

        IUserRepository GetUserRepository();
    }
}
