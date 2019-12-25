using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.ValueTypes
{
    public class CChange
    {
        public Int32 Id { get; }

        public CUser User { get; } 

        public CChecker Checker { get; }

        public DateTime DateMoving { get; }


        public CChange(Int32 id, CPlayer player, CChecker checker)
        {
            Id = id;
            User = player;
            Checker = checker;

            // TODO: sync time
            DateMoving = DateTime.Now;
        }
    }
}
