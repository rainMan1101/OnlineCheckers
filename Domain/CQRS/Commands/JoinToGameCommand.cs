using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CJoinToGameCommand : ICommand<CGuest>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CJoinToGameCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CGuest guest)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var guestRepository = uow.GetGuestRepository();
                try
                {
                    guestRepository.Add(guest);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }
    }
}
