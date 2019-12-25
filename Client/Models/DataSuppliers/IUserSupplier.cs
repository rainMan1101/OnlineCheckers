using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models.DataSuppliers
{
    public interface IUserSupplier
    {
        //  Список всех созданных игр
        List<CGameInfo> GetAllGames();

        //  Информация о конкретном пользователе
        CUser GetUserInfo();

        Guid CreateNewGame(String gameName, ETeamType teamType);

        void JoinToGame(Guid gameId);
    }
}
