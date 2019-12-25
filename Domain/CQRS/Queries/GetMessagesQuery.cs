using Domain.CQRS.Contexts;
using Domain.Entities;
using Domain.Services.UnitOfWork;
using Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class CGetMessagesQuery : IMultipleResultQuery<CGetMessageContext, CMessage>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetMessagesQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public IEnumerable<CMessage> Ask(CGetMessageContext context)
        {
            IEnumerable<CMessage> messages;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var messagesRepository = uow.GetMessageRepository();

                try
                {
                    messages = messagesRepository.GetAll(context.Game, context.StartId, context.EndId);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return messages;
        }
    }
}
