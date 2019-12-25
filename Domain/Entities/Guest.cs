using Domain.ValueTypes;
using System;

namespace Domain.Entities
{
    public class CGuest : CUser
    {
        public CGame Game { get; }

        public CGuest(CUser user, CGame game) : base(user)
        {
            Game = game;
        }

        public CGuest(CGuest guest) : base(guest)
        {
            Game = guest.Game;
        }
    }
}
