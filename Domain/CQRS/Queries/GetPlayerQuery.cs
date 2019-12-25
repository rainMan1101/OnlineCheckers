using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class CGetPlayerQuery : ISingleResultQuery<Guid, CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetPlayerQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public CPlayer Ask(Guid userId)
        {
            CPlayer player;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var playerRepository = uow.GetPlayerRepository();
                try
                {
                    player = playerRepository.Get(userId);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return player;
        }
    }
}
