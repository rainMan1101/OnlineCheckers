using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CChangeTeamCommand : ICommand<CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CChangeTeamCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Execute(CPlayer player)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var playerRepository = uow.GetPlayerRepository();
                var gameRepository = uow.GetGameRepository();

                try
                {
                    playerRepository.Update(player);

                    switch (player.TeamType)
                    {
                        case ETeamType.White:
                            if (gameRepository.BlackTeamPlayersCount(player.Game.Id) == 0)
                                player.Game.ChangeState(EGameState.Freeze);
                            break;

                        case ETeamType.Black:
                            if (gameRepository.WhiteTeamPlayersCount(player.Game.Id) == 0)
                                player.Game.ChangeState(EGameState.Freeze);
                            break;

                        default: throw new ArgumentOutOfRangeException();
                    }

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
