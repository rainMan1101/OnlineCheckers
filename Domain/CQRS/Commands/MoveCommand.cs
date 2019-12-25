using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueTypes;

namespace Domain.CQRS.Commands
{
    public class CMoveCommand : ICommand<CChange>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CMoveCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CChange change)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var checkerRepository = uow.GetCheckerRepository();
                var changeRepository = uow.GetChangeRepository();

                try
                {
                    checkerRepository.Update(change.Checker);
                    changeRepository.Add(change);
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
