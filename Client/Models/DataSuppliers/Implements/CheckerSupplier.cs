using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers.Implements
{
    public class CheckerSupplier : ICheckerSupplier
    {
        public CheckerSupplier(Guid userId, Guid gameId)
        {

        }

        public List<Checker> GetBoardState()
        {
            throw new NotImplementedException();
        }

        public int GetCountChanges(int lastChange)
        {
            throw new NotImplementedException();
        }

        public List<Checker> GetLastChanges(int lastChange)
        {
            throw new NotImplementedException();
        }

        public void SetChange(Checker checker)
        {
            throw new NotImplementedException();
        }
    }
}
