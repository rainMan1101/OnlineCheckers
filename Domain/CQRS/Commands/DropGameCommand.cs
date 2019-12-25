using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CDropGameCommand : ICommand<CGame>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CDropGameCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CGame game)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var gameRepository = uow.GetGameRepository();
                var userRepository = uow.GetUserRepository();

                try
                {
                    gameRepository.Delete(game.Id); // каскадное удаление из checkers, move и message
                    userRepository.DeleteMarksUsers(); 
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
