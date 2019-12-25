using System;
using System.Data;
using Domain.ValueTypes;
using Domain.Services.Repositories;
using System.Collections.Generic;
using Domain.Entities;

namespace OnlineCheckers.DataStorage.Repositories
{
    public class CMessageRepository : IMessageRepository
    {
        private readonly IDbTransaction _transaction;

        public CMessageRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(CMessage message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CMessage> GetAll(CGame game, IEnumerable<CUser> users, int starId, int count)
        {
            throw new NotImplementedException();
        }

        public void IsExist(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
