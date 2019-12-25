using Common;
using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.DataStorage.UnitOfWork
{
    public class OnlineCheckersUnitOfWorkFactory
    {
        private readonly CDataStorageSettings _settings;

        public OnlineCheckersUnitOfWorkFactory(CDataStorageSettings settings)
        {
            CGuard.IsNotNull(settings, nameof(settings));
            _settings = settings;
        }

        public IOnlineCheckersUnitOfWork Create()
        {
            var connection = new SqlConnection(_settings.ConnectionString);

            connection.Open();

            return new COnlineCheckersUnitOfWork(connection);
        }
    }
}
