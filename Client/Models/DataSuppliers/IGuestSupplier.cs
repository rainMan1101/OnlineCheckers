using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCheckers.Client.Models.Enums;

namespace OnlineCheckers.Client.Models.DataSuppliers
{
    public interface IGuestSupplier
    {
        List<CUser> GetGuests();

        List<CPlayer> GetPlayers();

        void JoinToCommand(Guid userId, ETeamType teamType);
    }
}
