using System.Data.SqlClient;

namespace OnlineCheckers.DataStorage.Mappers
{
    internal interface IMapper<out T>
    {
        T ReadItem(SqlDataReader rd);
    }
}
