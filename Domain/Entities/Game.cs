using System;

namespace Domain.Entities
{
    public class CGame
    {
        public Guid Id { get; }

        public String Name { get; }

        public EGameState State { get; private set; }

        public DateTime StartMoving { get; private set; }


        public CGame(Guid id, String gameName)
        {
            Id = id;
            Name = gameName;
            State = EGameState.Freeze;
            StartMoving = new DateTime();
        }

        public void ChangeState(EGameState state)
        {
            State = state;
        }

        public void SetStartMoving(DateTime startDate)
        {
            StartMoving = startDate;
        }
    }
}
