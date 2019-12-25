using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CLeaveTeamCommand : ICommand<CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CLeaveTeamCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
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
                    playerRepository.Delete(player.Id);
                    Boolean isWhiteTeamEmpty = gameRepository.WhiteTeamPlayersCount(player.Game.Id) == 0;
                    Boolean isBlackTeamEmpty = gameRepository.BlackTeamPlayersCount(player.Game.Id) == 0;


                    if (isWhiteTeamEmpty || isBlackTeamEmpty)
                    {
                        if (isWhiteTeamEmpty && isBlackTeamEmpty)
                            player.Game.ChangeState(EGameState.Close);
                        else
                        {
                            player.Game.ChangeState(EGameState.Freeze);
                            gameRepository.Update(player.Game);
                        }
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
