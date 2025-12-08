using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
    public static class OrderItemRepository
    {
        public static async Task<IReadOnlyList<OrderItemLine>> GetOrderItemLinesAsync(int orderId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<OrderItemLine>().Where(cl => cl.OrderId == orderId).ToListAsync(cancellationToken);
        }

        public static async Task AddLineAsync(OrderItemLine line, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.InsertAsync(line);
        }

        public static async Task RemoveLineAsync(int orderId, int variantId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<OrderItemLine>().Where(cl => cl.OrderId == orderId && cl.VariantId == variantId).DeleteAsync(cancellationToken);
        }

        public static async Task AssignToOrderAsync(int orderId, int variantId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<OrderItemLine>().Where(cl => cl.VariantId == variantId).Set(cl => cl.OrderId, orderId).UpdateAsync(cancellationToken);
        }
    }
}