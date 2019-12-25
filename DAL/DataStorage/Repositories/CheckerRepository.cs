using System;
using System.Collections.Generic;
using System.Data;
using Domain.Entities;
using Domain.Services.Repositories;

namespace OnlineCheckers.DataStorage.Repositories
{
    public class CCheckerRepository : ICheckerRepository
    {
        private readonly IDbTransaction _transaction;

        public CCheckerRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void AddAll(CChecker[] checker)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CChecker> GetAll(CGame game)
        {
            throw new NotImplementedException();
        }

        public void Update(CChecker checker)
        {
            throw new NotImplementedException();
        }
    }
}
