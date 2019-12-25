using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Services.UnitOfWork;

namespace Domain.CQRS.Queries
{
    public class CGetGameQuery : ISingleResultQuery<Guid, CGame>
    {
        IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CGetGameQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public CGame Ask(Guid gameId)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                try
                {
                    var gameRepository = uow.GetGameRepository();
                    CGame game = gameRepository.Get(gameId);
                    uow.Commit();
                    return game;
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
