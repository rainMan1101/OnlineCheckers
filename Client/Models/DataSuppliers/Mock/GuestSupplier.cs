using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models.DataSuppliers.Mock
{
    public class GuestSupplier : IGuestSupplier
    {
        private Guid _gameId;

        public GuestSupplier(Guid gameId)
        {
            _gameId = gameId;
        }

        public List<CUser> GetGuests()
        {
            throw new NotImplementedException();
        }

        public List<CPlayer> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public void JoinToCommand(Guid userId, ETeamType teamType)
        {
            throw new NotImplementedException();
        }
    }
}
