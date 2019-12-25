using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class CGetUserQuery : ISingleResultQuery<Guid, CUser>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetUserQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public CUser Ask(Guid userId)
        {
            CUser user;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var userRepository = uow.GetUserRepository();
                try
                {
                    user = userRepository.Get(userId);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return user;
        }
    }
}
