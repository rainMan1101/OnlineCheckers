using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebService.Threading;

namespace WebService.Hubs
{
    public class CGames
    {
        public static CSynchronizedCache<Guid, CGame> Games { get; set; }
            = new CSynchronizedCache<Guid, CGame>();

    }
}