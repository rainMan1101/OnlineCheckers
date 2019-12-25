using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Entities;

namespace Domain.Services.Repositories
{
    public interface ICheckerRepository
    {
        IEnumerable<CChecker> GetAll(CGame game);

        void AddAll(CChecker[] checker);

        void Update(CChecker checker);
    }
}
