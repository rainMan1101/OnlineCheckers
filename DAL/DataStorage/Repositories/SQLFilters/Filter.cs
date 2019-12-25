using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.Repositories.Filters;

namespace OnlineCheckers.DataStorage.Repositories.SQLFilters
{
    public class CFilter
    {
        public String GetSQLExpression() => _sqlExpression.ToString();

        public SqlParameter[] GetSqlParameters() => _sqlParameters.ToArray();


        private StringBuilder _sqlExpression = new StringBuilder();

        private List<SqlParameter> _sqlParameters = new List<SqlParameter>();


        protected void SetField(String sqlName, Object value, SqlDbType type, EOperationType operationType)
        {
            AppendToSqlExpression(operationType, sqlName, value);
            _sqlParameters.Add(new SqlParameter(sqlName, type) { Value = value });
        }

        private void AppendToSqlExpression(EOperationType operationType, String parameterName, Object value)
        {
            String operationString = GetOperationString(operationType, parameterName, value);

            if (_sqlExpression.Length == 0)
            {
                _sqlExpression.Append(" ");
                _sqlExpression.Append(operationString);
            }
            else
            {
                _sqlExpression.Append(" AND ");
                _sqlExpression.Append(operationString);
                _sqlExpression.Append(" ");
            }
        }

        private String GetOperationString(EOperationType operationType, String parameterName, Object value)
        {
            switch (operationType)
            {
                case EOperationType.Equals:
                    return $"{parameterName} = {value}";

                case EOperationType.NoEquals:
                    return $"{parameterName} <> {value}";

                case EOperationType.Bellow:
                    return $"{parameterName} < {value}";

                case EOperationType.Above:
                    return $"{parameterName} > {value}";

                case EOperationType.BellowEquals:
                    return $"{parameterName} <= {value}";

                case EOperationType.AboveEquals:
                    return $"{parameterName} >= {value}";

                // TODO: log
                default: throw new Exception();
            }
        }

    }
}
