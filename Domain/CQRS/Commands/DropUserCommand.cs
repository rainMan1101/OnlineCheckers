using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services.UnitOfWork;

namespace Domain.CQRS.Commands
{
    public class DropUserCommand : ICommand<Guid>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public DropUserCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(Guid userId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var userRepository = uow.GetUserRepository();
                var messageRepository = uow.GetMessageRepository();
                var changeRepository = uow.GetChangeRepository();

                try
                {
                    if (messageRepository.IsExist(userId) || changeRepository.IsExist(userId))
                        userRepository.MarkToDelete(userId);
                    else
                        userRepository.Delete(userId);

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
