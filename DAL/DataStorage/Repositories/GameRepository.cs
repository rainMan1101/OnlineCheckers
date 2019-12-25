using System;
using System.Collections.Generic;
using System.Data;
using Domain.Entities;
using Domain.Services.Repositories;

namespace OnlineCheckers.DataStorage.Repositories 
{
    public class CGameRepository : IGameRepository
    {
        private readonly IDbTransaction _transaction;

        public CGameRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(CGame game)
        {
            throw new NotImplementedException();
        }

        public int BlackTeamPlayersCount(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public CGame Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CGame> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(CGame game)
        {
            throw new NotImplementedException();
        }

        public int WhiteTeamPlayersCount(Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
