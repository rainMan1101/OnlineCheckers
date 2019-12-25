using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ValueTypes
{
    public class CMessage
    {
        public Int32 Id { get; }

        public CGame Game { get; }

        public String Text { get; }

        public CUser Sender { get; }

        public CMessage(Int32 id, CGuest guest, String messageText)
        {
            Id = id;
            Game = guest.Game; // guest may change game, but message stay
            Sender = guest;
            Text = messageText;
        }
    }
}
