using OnlineCheckers.DataStorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCheckers.DataStorage.Mappers;

namespace OnlineCheckers.DataStorage.Repositories
{
    internal static class CDBHelper
    {
        public static T GetItem<T>(IMapper<T> mapper, IDbTransaction transaction,
            String sql, params SqlParameter[] sqlParameters)
        {
            var result = default(T);

            if (transaction.Connection.State != ConnectionState.Open)
                transaction.Connection.Open();

            var sqlTransaction = (SqlTransaction)transaction;

            using (var sqlCommand = new SqlCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                sqlCommand.Parameters.AddRange(sqlParameters);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                        result = mapper.ReadItem(reader);
                }
            }

            return result;
        }


        public static IEnumerable<T> GetItems<T>(IMapper<T> mapper, IDbTransaction transaction,
            String sql, params SqlParameter[] sqlParameters)
        {
            var results = new List<T>();

            if (transaction.Connection.State != ConnectionState.Open)
                transaction.Connection.Open();

            var sqlTransaction = (SqlTransaction)transaction;

            using (var sqlCommand = new SqlCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                sqlCommand.Parameters.AddRange(sqlParameters);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                        results.Add(mapper.ReadItem(reader));
                }
            }

            return results;
        }


        public static void Execute(IDbTransaction transaction, String sql, params SqlParameter[] sqlParameters)
        {
            if (transaction.Connection.State != ConnectionState.Open)
                transaction.Connection.Open();

            var sqlTransaction = (SqlTransaction)transaction;

            using (var sqlCommand = new SqlCommand(sql, sqlTransaction.Connection, sqlTransaction))
            {
                sqlCommand.Parameters.AddRange(sqlParameters);
                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
