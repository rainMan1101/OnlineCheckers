using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CLeaveGameCommand : ICommand<Guid>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CLeaveGameCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(Guid guestId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var guestRepository = uow.GetGuestRepository();

                try
                {
                    guestRepository.Delete(guestId);

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
