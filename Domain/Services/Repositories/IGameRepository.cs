using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Services.Repositories
{
    public interface IGameRepository
    {
        IEnumerable<CGame> GetAll();

        CGame Get(Guid Id);

        Int32 WhiteTeamPlayersCount(Guid Id);

        Int32 BlackTeamPlayersCount(Guid Id);

        void Add(CGame game);

        void Update(CGame game);

        void Delete(Guid gameId);
    }
}
