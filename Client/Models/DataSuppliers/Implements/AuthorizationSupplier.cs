using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers.Implements
{
    public class AuthorizationSupplier : IAuthorizationSupplier
    {
        public Guid Authorizate(String userName)
        {
            return Guid.NewGuid();
        }
    }
}
