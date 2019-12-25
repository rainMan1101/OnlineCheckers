using System;
using System.Data.SqlClient;
using OnlineCheckers.DTO;
using OnlineCheckers.DTO.Enums;

namespace OnlineCheckers.DataStorage.Mappers
{
    internal class CUserDTOMapper : IMapper<CUserDTO>
    {
        public CUserDTO ReadItem(SqlDataReader rd)
        {
            return new CUserDTO
            {
                Id = (Guid)rd["id"],
                Name = (String)rd["name"]
            };
        }
    }
}
