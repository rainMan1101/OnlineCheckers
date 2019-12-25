using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage
{
    public class CDataStorageSettings
    {
        public string ConnectionString { get; }


        public CDataStorageSettings(String connectionString)
        {
            CGuard.IsNotNullOrEmpty(connectionString, nameof(connectionString));
            CGuard.IsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

            ConnectionString = connectionString;
        }
    }
}
