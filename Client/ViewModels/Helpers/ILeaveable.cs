using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ViewModels
{
    public interface ILeaveable
    {
        event EventHandler Leave;
    }
}
