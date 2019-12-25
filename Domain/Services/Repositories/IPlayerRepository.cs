using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Repositories
{
    public interface IPlayerRepository
    {
        IEnumerable<CPlayer> GetAll(CGame game);

        CPlayer Get(Guid id);

        void Add(CPlayer player);

        void Update(CPlayer player);

        void Delete(Guid id);
    }
}
