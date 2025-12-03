using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public sealed class OrderRepository
    {
        public async Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Order>().FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetByCustomerAsync(CustomerId customerId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Order>().Where(o => o.CustomerId == customerId).ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var identity = await db.InsertWithIdentityAsync(order);
            if (identity != null)
            {
                try
                {
                    order.Id = (OrderId)Convert.ToInt32(identity);
                }
                catch { }
            }
        }

        public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(order);
        }

        public async Task DeleteAsync(OrderId id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Order>().Where(o => o.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
