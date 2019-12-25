using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Hubs
{
    public class CGameInfo
    {
        public CGame Game { get; }

        public Int32 LastMessageIndex { get; set; }


        public CGameInfo(CGame game)
        {
            Game = game;
            LastMessageIndex = 1;
        }
    }
}