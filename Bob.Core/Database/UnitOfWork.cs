using LinqToDB;
using LinqToDB.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Database
{
#nullable enable
    public sealed class UnitOfWork : IAsyncDisposable
    {
        private readonly DataConnection m_Connection;
        private DataConnectionTransaction? m_Transaction;

        public UnitOfWork()
        {
            m_Connection = new AppDataConnection();
        }

        public DataConnection Connection => m_Connection;

        public async Task BeginAsync(CancellationToken cancellationToken = default)
        {
            if (m_Transaction != null) return;
            m_Transaction = await m_Connection.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (m_Transaction == null) return;
            await m_Transaction.CommitAsync(cancellationToken);
            await m_Transaction.DisposeAsync();
            m_Transaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (m_Transaction == null) return;
            await m_Transaction.RollbackAsync(cancellationToken);
            await m_Transaction.DisposeAsync();
            m_Transaction = null;
        }

        public ValueTask DisposeAsync()
        {
            if (m_Transaction != null)
            {
                // best-effort rollback
                try { m_Transaction.Rollback(); } catch { }
                m_Transaction.Dispose();
            }
            m_Connection.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
