using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Domain.Entities
{
    public enum EGameState
    {
        WhiteTeamMoveWaiting,
        BlackTeamdMoveWaiting,
        MoveDoing,
        Freeze,
        Close
    }
}