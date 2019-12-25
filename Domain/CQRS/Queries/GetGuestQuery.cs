using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class CGetGuestQuery : ISingleResultQuery<Guid, CGuest>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetGuestQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public CGuest Ask(Guid userId)
        {
            CGuest guest;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var guestRepo = uow.GetGuestRepository();
                try
                {
                    guest = guestRepo.Get(userId);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return guest;
        }
    }
}
