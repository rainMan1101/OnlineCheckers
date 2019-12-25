using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries
{
    public class GetPlayersQuery : IMultipleResultQuery<CGame, CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public GetPlayersQuery(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public IEnumerable<CPlayer> Ask(CGame game)
        {
            IEnumerable<CPlayer> players;

            using (var uow = _unitOfWorkFactory.Create())
            {
                var playerRepository = uow.GetPlayerRepository();
                try
                {
                    players = playerRepository.GetAll(game);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }

            return players;
        }
    }
}
