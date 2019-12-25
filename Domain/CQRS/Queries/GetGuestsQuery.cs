using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class GetGuestsQuery : IMultipleResultQuery<CGame, CGuest>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public GetGuestsQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public IEnumerable<CGuest> Ask(CGame game)
        {
            IEnumerable<CGuest> guests;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var guestRepo = uow.GetGuestRepository();
                try
                {
                    guests = guestRepo.GetAll(game);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return guests;
        }
    }
}
