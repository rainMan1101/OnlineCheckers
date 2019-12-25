using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Hubs
{
    public class CGameActivity
    {
        public CPlayer Player { get;}

        public CChecker Checker { get; }

        public CGameActivity(CPlayer player, CChecker checker)
        {
            Player = player;
            Checker = checker;
        }
    }
}