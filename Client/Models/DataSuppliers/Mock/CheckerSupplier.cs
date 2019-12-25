using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.Models.DataSuppliers.Mock
{
    public class CheckerSupplier : ICheckerSupplier
    {
        private Guid _userId;

        private Guid _gameId;

        private List<Checker> _boardState;

        private List<Checker>[] _changes;

        private Int32 _changesNumber;

        private Boolean _isSetChanges;


        public CheckerSupplier(Guid userId, Guid gameId)
        {
            _userId = userId;
            _gameId = gameId;

            _boardState = new List<Checker>(24);
            _boardState.Add(new Checker { Number = 0, X = 55, Y = 5, Alive = true });
            _boardState.Add(new Checker { Number = 1, X = 155, Y = 5, Alive = true });
            _boardState.Add(new Checker { Number = 2, X = 255, Y = 5, Alive = true });
            _boardState.Add(new Checker { Number = 3, X = 355, Y = 5, Alive = true });
            _boardState.Add(new Checker { Number = 4, X = 5, Y = 55, Alive = true });
            _boardState.Add(new Checker { Number = 5, X = 105, Y = 55, Alive = true });
            _boardState.Add(new Checker { Number = 6, X = 205, Y = 55, Alive = true });
            _boardState.Add(new Checker { Number = 7, X = 305, Y = 55, Alive = true });
            _boardState.Add(new Checker { Number = 8, X = 55, Y = 105, Alive = true });
            _boardState.Add(new Checker { Number = 9, X = 155, Y = 105, Alive = true });
            _boardState.Add(new Checker { Number = 10, X = 255, Y = 105, Alive = true });
            _boardState.Add(new Checker { Number = 11, X = 355, Y = 105, Alive = true });

            _boardState.Add(new Checker { Number = 12, X = 5, Y = 255, Alive = true });
            _boardState.Add(new Checker { Number = 13, X = 105, Y = 255, Alive = true });
            _boardState.Add(new Checker { Number = 14, X = 205, Y = 255, Alive = true });
            _boardState.Add(new Checker { Number = 15, X = 305, Y = 255, Alive = true });
            _boardState.Add(new Checker { Number = 16, X = 55, Y = 305, Alive = true });
            _boardState.Add(new Checker { Number = 17, X = 155, Y = 305, Alive = true });
            _boardState.Add(new Checker { Number = 18, X = 255, Y = 305, Alive = true });
            _boardState.Add(new Checker { Number = 19, X = 355, Y = 305, Alive = true });
            _boardState.Add(new Checker { Number = 20, X = 5, Y = 355, Alive = true });
            _boardState.Add(new Checker { Number = 21, X = 105, Y = 355, Alive = true });
            _boardState.Add(new Checker { Number = 22, X = 205, Y = 355, Alive = true });
            _boardState.Add(new Checker { Number = 23, X = 305, Y = 355, Alive = true });

            _isSetChanges = false;

            _changes = new List<Checker>[5];

            _changes[0] = new List<Checker>(1);
            _changes[0].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });

            _changes[1] = new List<Checker>(2);
            _changes[1].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[1].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });

            _changes[2] = new List<Checker>(3);
            _changes[2].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[2].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[2].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });

            _changes[3] = new List<Checker>(5);
            _changes[3].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[3].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[3].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[3].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });
            _changes[3].Add(new Checker { Number = 23, X = 0, Y = 0, Alive = false });

            _changes[4] = new List<Checker>(0);

        }

        public List<Checker> GetBoardState()
        {
            return _boardState;
        }

        public int GetCountChanges(int lastChange)
        {
            _isSetChanges = true;
            Random random = new Random();
            _changesNumber = random.Next(0, 5);
            return _changes[_changesNumber].Count;
        }

        public List<Checker> GetLastChanges(int lastChange)
        {
            if (!_isSetChanges)
            {
                Random random = new Random();
                _changesNumber = random.Next(0, 6);
            }

            return _changes[_changesNumber];
        }

        public void SetChange(Checker checker)
        {
            _boardState[checker.Number] = checker;
        }
    }
}
