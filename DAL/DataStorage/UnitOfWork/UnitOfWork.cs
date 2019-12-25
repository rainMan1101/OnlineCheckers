using Domain.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace OnlineCheckers.DataStorage.UnitOfWork
{
    public class CUnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection { get; private set; }

        public IDbTransaction Transaction { get; private set; }

        private Boolean _disposed;


        internal CUnitOfWork(IDbConnection connection)
        {
            CGuard.IsNotNull(connection, nameof(connection));
            CGuard.IsValid(connection.State == ConnectionState.Open, "connection.State != ConnectionState.Open");

            Connection = connection;

            Transaction = Connection.BeginTransaction();
        }

        public void Commit()
        {
            Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            Transaction.Rollback();
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Transaction != null)
                    {
                        Transaction.Dispose();
                        Transaction = null;
                    }
                    if (Connection != null)
                    {
                        Connection.Dispose();
                        Connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~CUnitOfWork()
        {
            Dispose(false);
        }
    }
}
