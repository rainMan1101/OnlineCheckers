using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CQRS.Contexts
{
    public class CGetTeamPlayersCountContext
    {
        public Guid GameId { get; }

        public ETeamType TeamType { get; }


        public CGetTeamPlayersCountContext(Guid gameId, ETeamType teamType)
        {
            GameId = gameId;
            TeamType = teamType;
        }
    }
}
