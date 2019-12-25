using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers
{
    public interface IPlayerSupplier
    {
        // Возвращает временный токен игрока для входа
        Guid GetToken();

        Boolean IsValidToken(Guid token);

        void LeaveTeam();
    }
}
