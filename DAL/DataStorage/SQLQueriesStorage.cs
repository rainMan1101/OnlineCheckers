using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage
{
    internal static class CSQLQueriesStorage
    {
        public static Dictionary<String, SQLQuery> Queries { get; set; }    

        static CSQLQueriesStorage()
        {
            Queries = new Dictionary<String, SQLQuery>();

            Queries.Add("AddUser", new SQLQuery(
                @"INSERT INTO users VALUES(@userId, @userName, 0)",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid), new SqlParameter("@userName", DbType.String) } ));

            Queries.Add("DeleteUser", new SQLQuery(
                @"DELETE FROM users WHERE id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));

            Queries.Add("GetUser", new SQLQuery(
                @"SELECT id, name FROM user WHERE id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));

            Queries.Add("MarkToDeleteUser", new SQLQuery(
                @"UPDATE users SET is_deleted = 1 WHERE id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));

            Queries.Add("DeleteMarksUsers", new SQLQuery(
                @"DELETE u FROM users u 
                LEFT JOIN messages m ON m.user_id = u.id 
                LEFT JOIN changes c ON c.user_id = u.id
                WHERE u.is_deleted = 1 AND m.user_id IS NULL AND c.user_id IS NULL"));

            Queries.Add("MarkToDelete", new SQLQuery(
                @"UPDATE users SET is_deleted = 1 WHERE id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));


            Queries.Add("AddGuest", new SQLQuery(
                @"INSERT INTO guests VALUES(@userId, @gameId)",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid), new SqlParameter("@gameId", DbType.Guid) }));

            Queries.Add("DeleteGuest", new SQLQuery(
                @"DELETE FROM guests WHERE user_id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));


            Queries.Add("GetGuest", new SQLQuery(
                @"SELECT u.id uid, u.name uname, g.id gid, g.name gname FROM guests 
                INNER JOIN games g ON guests.user_id = g.Id
                INNER JOIN users u ON guests.game_id = u.Id
                WHERE guests.user_id = @userId",
                new SqlParameter[] { new SqlParameter("@userId", DbType.Guid) }));

            Queries.Add("GetGuests", new SQLQuery(
                @"SELECT u.id uid, u.name uname FROM guests 
                INNER JOIN users u ON guests.game_id = u.Id
                WHERE guests.game_id = @gameId",
                new SqlParameter[] { new SqlParameter("@gameId", DbType.Guid) }));
        }
    }
}
