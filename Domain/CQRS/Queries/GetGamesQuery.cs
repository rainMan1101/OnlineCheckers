using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Services.UnitOfWork;

namespace Domain.CQRS.Queries
{
    public class CGetGamesQuery : IMultipleResultQuery<CGame>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetGamesQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IEnumerable<CGame> Ask()
        {
            IEnumerable<CGame> games;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var gameRepo = uow.GetGameRepository();
                try
                {
                    games = gameRepo.GetAll();
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return games; // Can't be null (or exception)
        }
    }
}
