using System;

namespace Domain.Entities
{
    public class CPlayer : CGuest
    {
        public ETeamType TeamType { get; private set; }

        public Boolean IsActive { get; private set; }


        public CPlayer(CGuest guest, ETeamType teamType) : base(guest)
        {
            TeamType = teamType;
        }

        public void ChangeTeam(ETeamType teamType)
        {
            TeamType = teamType;
        }

        public void BeActive()
        {
            IsActive = true;
        }

        public void NotBeActive()
        {
            IsActive = false;
        }
    }
}
