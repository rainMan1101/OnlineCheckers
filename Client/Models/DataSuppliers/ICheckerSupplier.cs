using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers
{
    public interface ICheckerSupplier
    {
        Int32 GetCountChanges(int lastChange);

        void SetChange(Checker checker);

        // Last 5 changes
        List<Checker> GetLastChanges(int lastChange);

        List<Checker> GetBoardState();
    }
}
