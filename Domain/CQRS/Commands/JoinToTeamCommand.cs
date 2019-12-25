using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CJoinToTeamCommand : ICommand<CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CJoinToTeamCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
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
                    playerRepository.Add(player);


                    switch (player.TeamType)
                    {
                        case ETeamType.White:
                            if (gameRepository.WhiteTeamPlayersCount(player.Game.Id) == 1)
                                player.Game.ChangeState(EGameState.WhiteTeamMoveWaiting);
                            break;

                        case ETeamType.Black:
                            if (gameRepository.BlackTeamPlayersCount(player.Game.Id) == 1)
                                player.Game.ChangeState(EGameState.BlackTeamdMoveWaiting);
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
