using OnlineCheckers.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage.Mappers
{
    internal class GuestDTOMapper : IMapper<CGuestDTO>
    {
        public CGuestDTO ReadItem(SqlDataReader rd)
        {
            return new CGuestDTO
            {
                UserId = (Guid)rd["uid"],
                UserName = (String)rd["uname"],
                GameId = (Guid)rd["gid"],
                GameName = (String)rd["gname"]

            };
        }
    }
}
