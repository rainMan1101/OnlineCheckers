using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers
{
    public interface IAuthorizationSupplier
    {
        Guid Authorizate(String userName);
    }
}
