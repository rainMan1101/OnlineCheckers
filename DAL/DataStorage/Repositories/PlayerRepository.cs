using System;
using System.Collections.Generic;
using System.Data;
using Domain.Entities;
using Domain.Services.Repositories;

namespace OnlineCheckers.DataStorage.Repositories
{
    public class CPlayerRepository : IPlayerRepository
    {
        private readonly IDbTransaction _transaction;

        public CPlayerRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(CPlayer player)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public CPlayer Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CPlayer> GetAll(CGame game)
        {
            throw new NotImplementedException();
        }

        public void Update(CPlayer player)
        {
            throw new NotImplementedException();
        }
    }
}
