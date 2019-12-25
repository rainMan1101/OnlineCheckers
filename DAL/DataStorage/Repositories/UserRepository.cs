using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Domain.Entities;
using Domain.Services.Repositories;
using OnlineCheckers.DataStorage.Mappers;

namespace OnlineCheckers.DataStorage.Repositories
{
    public class CUserRepository : IUserRepository
    {
        private readonly IDbTransaction _transaction;

        public CUserRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(CUser user)
        {
            var parameters = CSQLQueriesStorage.Queries["AddUser"].SqlParameters;
            parameters[0].Value = user.Id;
            parameters[1].Value = user.Name;

            CDBHelper.Execute(_transaction, 
                CSQLQueriesStorage.Queries["AddUser"].SQLString, parameters);
        }

        public void Delete(Guid id)
        {
            var parameters = CSQLQueriesStorage.Queries["AddUser"].SqlParameters;
            parameters[0].Value = id;

            CDBHelper.Execute(_transaction,
                CSQLQueriesStorage.Queries["DeleteUser"].SQLString, parameters);
        }

        public void DeleteMarksUsers()
        {
            CDBHelper.Execute(_transaction,
                CSQLQueriesStorage.Queries["DeleteMarksUsers"].SQLString);
        }


        public CUser Get(Guid id)
        {
            var parameters = CSQLQueriesStorage.Queries["GetUser"].SqlParameters;
            parameters[0].Value = id;

            var userDTO =  CDBHelper.GetItem(new CUserDTOMapper(), _transaction,
                CSQLQueriesStorage.Queries["GetUser"].SQLString, parameters);

            return new CUser(userDTO.Id, userDTO.Name);
        }

        public void MarkToDelete(Guid id)
        {
            var parameters = CSQLQueriesStorage.Queries["GetUser"].SqlParameters;
            parameters[0].Value = id;

            CDBHelper.Execute(_transaction,
                CSQLQueriesStorage.Queries["MarkToDelete"].SQLString, parameters);
        }
    }
}
