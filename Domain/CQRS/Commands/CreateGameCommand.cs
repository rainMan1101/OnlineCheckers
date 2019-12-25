using Domain.Entities;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Commands
{
    public class CCreateGameCommand : ICommand<CPlayer>
    {
        private IOnlineCheckersUnitOfWorkFactory _unitOfWorkFactory;

        public CCreateGameCommand(IOnlineCheckersUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }


        public void Execute(CPlayer player)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var gameRepository = uow.GetGameRepository();
                var checkerRepository = uow.GetCheckerRepository();
                var playerRepository = uow.GetPlayerRepository();

                try
                {
                    gameRepository.Add(player.Game);
                    playerRepository.Add(player);
                    checkerRepository.AddAll(new CChecker[]
                    {
                        new CChecker(id: 1, game: player.Game, x: 55, y: 5),
                        new CChecker(id: 2, game: player.Game, x: 155, y: 5),
                        new CChecker(id: 3, game: player.Game, x: 255, y: 5),
                        new CChecker(id: 4, game: player.Game, x: 355, y: 5),

                        new CChecker(id: 5, game: player.Game, x: 5, y: 55),
                        new CChecker(id: 6, game: player.Game, x: 105, y: 55),
                        new CChecker(id: 7, game: player.Game, x: 205, y: 55),
                        new CChecker(id: 8, game: player.Game, x: 305, y: 55),

                        new CChecker(id: 9, game: player.Game, x: 55, y: 105),
                        new CChecker(id: 10, game: player.Game, x: 155, y: 105),
                        new CChecker(id: 11, game: player.Game, x: 255, y: 105),
                        new CChecker(id: 12, game: player.Game, x: 355, y: 105),

                        new CChecker(id: 13, game: player.Game, x: 5, y: 255),
                        new CChecker(id: 14, game: player.Game, x: 105, y: 255),
                        new CChecker(id: 15, game: player.Game, x: 205, y: 255),
                        new CChecker(id: 16, game: player.Game, x: 305, y: 255),

                        new CChecker(id: 17, game: player.Game, x: 55, y: 305),
                        new CChecker(id: 18, game: player.Game, x: 155, y: 305),
                        new CChecker(id: 19, game: player.Game, x: 255, y: 305),
                        new CChecker(id: 20, game: player.Game, x: 355, y: 305),

                        new CChecker(id: 21, game: player.Game, x: 5, y: 355),
                        new CChecker(id: 22, game: player.Game, x: 105, y: 355),
                        new CChecker(id: 23, game: player.Game, x: 205, y: 355),
                        new CChecker(id: 24, game: player.Game, x: 305, y: 355)
                    });
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
