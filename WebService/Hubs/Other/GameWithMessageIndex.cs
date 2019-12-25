using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Hubs
{
    public class CGameCGameWithMessageIndex
    {
        public CGame Game { get; set; }
        public Int32 LastMessageIndex { get; set; }
    }
}