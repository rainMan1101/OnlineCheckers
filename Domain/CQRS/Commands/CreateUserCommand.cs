using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services.UnitOfWork;

namespace Domain.CQRS.Commands
{
    public class CreateUserCommand : ICommand<CUser>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CreateUserCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CUser user)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var userRepository = uow.GetUserRepository();
                try
                {
                    userRepository.Add(user);
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
