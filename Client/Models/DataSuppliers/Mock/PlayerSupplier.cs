using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models.DataSuppliers.Mock
{
    public class PlayerSupplier : IPlayerSupplier
    {
        public PlayerSupplier(Guid userId)
        {

        }

        public Guid GetToken()
        {
            return Guid.NewGuid();
        }

        public Boolean IsValidToken(Guid token)
        {
            throw new NotImplementedException();
        }

        public void LeaveTeam()
        {
            throw new NotImplementedException();
        }
    }
}
