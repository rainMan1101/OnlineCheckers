using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Contexts
{
    public class CGetMessageContext
    {
        public CGame Game { get; }
        public Int32 StartId { get; }
        public Int32 EndId { get; }

        public CGetMessageContext(CGame game, Int32 startId, Int32 endId)
        {
            if (startId < 0) startId = 0;
            if (endId < 0) endId = 0;

            Game = game;
            StartId = startId;
            EndId = endId;
        }
    } 
}
