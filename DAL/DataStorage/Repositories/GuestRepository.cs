using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Domain.Entities;
using Domain.Services.Repositories;
using OnlineCheckers.DataStorage.Mappers;

namespace OnlineCheckers.DataStorage.Repositories
{
    public class CGuestRepository : IGuestRepository
    {
        private readonly IDbTransaction _transaction;

        public CGuestRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(CGuest guest)
        {
            var parameters = CSQLQueriesStorage.Queries["AddGuest"].SqlParameters;
            parameters[0].Value = guest.Id;
            parameters[1].Value = guest.Game.Id;

            CDBHelper.Execute(_transaction, CSQLQueriesStorage.Queries["AddGuest"].SQLString, parameters);
        }

        public void Delete(Guid id)
        {
            var parameters = CSQLQueriesStorage.Queries["DeleteGuest"].SqlParameters;
            parameters[0].Value = id;

            CDBHelper.Execute(_transaction, CSQLQueriesStorage.Queries["DeleteGuest"].SQLString, parameters);
        }

        public CGuest Get(Guid id)
        {
            var parameters = CSQLQueriesStorage.Queries["GetGuest"].SqlParameters;
            parameters[0].Value = id;

            var guestDTO = CDBHelper.GetItem(
                new GuestDTOMapper(), 
                _transaction, 
                CSQLQueriesStorage.Queries["GetGuest"].SQLString, 
                parameters);

            return new CGuest(
                new CUser(guestDTO.UserId, guestDTO.UserName),
                new CGame(guestDTO.GameId, guestDTO.GameName));
        }

        public IEnumerable<CGuest> GetAll(CGame game)
        {
            var parameters = CSQLQueriesStorage.Queries["GetGuests"].SqlParameters;
            parameters[0].Value = game.Id;

            var guestLiteDTOs = CDBHelper.GetItems(
                new GuestLiteDTOMapper(), 
                _transaction, 
                CSQLQueriesStorage.Queries["GetGuests"].SQLString, 
                parameters);

            var guestList = new List<CGuest>();

            foreach (var guestLiteDTO in guestLiteDTOs)
            {
                var user = new CUser(guestLiteDTO.UserId, guestLiteDTO.UserName);
                guestList.Add(new CGuest(user, game));
            }

            return guestList;
        }
    }
}
