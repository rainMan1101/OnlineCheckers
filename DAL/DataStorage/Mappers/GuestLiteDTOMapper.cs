using OnlineCheckers.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage.Mappers
{
    internal class GuestLiteDTOMapper : IMapper<GuestLiteDTO>
    {
        public GuestLiteDTO ReadItem(SqlDataReader rd)
        {
            return new GuestLiteDTO
            {
                UserId = (Guid)rd["uid"],
                UserName = (String)rd["uname"]
            };
        }
    }
}
