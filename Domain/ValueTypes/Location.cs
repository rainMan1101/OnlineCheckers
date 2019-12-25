using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueTypes
{
    public struct CLocation
    {
        public Double X { get; }

        public Double Y { get; }

        public CLocation(Double x, Double y)
        {
            X = x;
            Y = y;
        }
    }
}
