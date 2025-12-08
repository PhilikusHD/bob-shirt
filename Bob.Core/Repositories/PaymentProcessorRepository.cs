using Bob.Core.Database;
using Bob.Core.Domain;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public static class PaymentProcessorRepository
    {
        public static async Task<PaymentProcessor?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<PaymentProcessor>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public static async Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<PaymentProcessor>().ToListAsync(cancellationToken);
        }
    }
}