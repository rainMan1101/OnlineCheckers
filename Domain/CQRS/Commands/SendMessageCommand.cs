using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueTypes;

namespace Domain.CQRS.Commands
{
    public class CSendMessageCommand : ICommand<CMessage>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CSendMessageCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CMessage message)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var messageRepository = uow.GetMessageRepository();

                try
                {
                    messageRepository.Add(message);
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
