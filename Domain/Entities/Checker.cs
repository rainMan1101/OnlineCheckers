using System;
using Domain.ValueTypes;


namespace Domain.Entities
{
    public class CChecker
    {
        public Int32 Id { get; }

        public CGame Game { get; }

        public Boolean IsAlive { get; private set; } 

        public Boolean IsKing { get; private set; } 

        public CLocation Location { get; private set; }


        public CChecker(Int32 id, CGame game, Double x, Double y)
        {
            if (id >= 1 && id <= 24)
            {
                Id = id;
                Game = game;
                IsAlive = true;
                IsKing = false;
                Location = new CLocation(x, y);
            }
            else
                throw new ArgumentException("Id шашки должно быть в пределах от 1 до 24");
        }

        public CChecker(Int32 id, CGame game, CLocation location)
        {
            if (id >= 1 && id <= 24)
            {
                Id = id;
                Game = game;
                IsAlive = true;
                IsKing = false;
                Location = location;
            }
            else
                throw new ArgumentException("Id шашки должно быть в пределах от 1 до 24");
        }


        public void Move(Double x, Double y)
        {
            Location = new CLocation(x, y);
        }

        public void Move(CLocation location)
        {
            Location = location;
        }

        public void BecomeKing()
        {
            IsKing = true;
        }

        public void Die()
        {
            IsAlive = false;
        }
    }
}
