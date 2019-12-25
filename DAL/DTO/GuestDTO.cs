using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCheckers.DTO
{
    public class CGuestDTO
    {
        public Guid UserId { get; set; }
        public String UserName { get; set; }

        public Guid GameId { get; set; }
        public String GameName { get; set; }
    }
}
