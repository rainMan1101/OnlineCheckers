using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Repositories
{
    public interface IGuestRepository
    {
        IEnumerable<CGuest> GetAll(CGame game);

        CGuest Get(Guid id);

        void Add(CGuest guest);

        void Delete(Guid id);
    }
}
