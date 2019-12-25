using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage
{
    internal class SQLQuery
    {
        public String SQLString { get; }

        public SqlParameter[] SqlParameters { get; }

        public SQLQuery(String sqlString, params SqlParameter[] sqlParameters)
        {
            SQLString = SQLString;
            SqlParameters = sqlParameters;
        }
    }
}
