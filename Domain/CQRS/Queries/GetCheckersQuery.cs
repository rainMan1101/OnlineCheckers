using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Services.UnitOfWork;

namespace Domain.CQRS.Queries
{
    public class CGetCheckersQuery : IMultipleResultQuery<CGame, CChecker>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetCheckersQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<CChecker> Ask(CGame game)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var checkersRepository = uow.GetCheckerRepository();
                    IEnumerable <CChecker> checkers = checkersRepository.GetAll(game);
                    uow.Commit();
                    return checkers;
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
