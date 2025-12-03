using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public sealed class PaymentRepository
    {
        public async Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Payment>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public async Task<Payment?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Payment>().FirstOrDefaultAsync(p => p.OrderId == orderId, cancellationToken);
        }

        public async Task AddAsync(Payment payment, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var identity = await db.InsertWithIdentityAsync(payment);
            if (identity != null)
            {
                try
                {
                    payment.Id = (int)Convert.ToInt32(identity);
                }
                catch { }
            }
        }
    }
}
