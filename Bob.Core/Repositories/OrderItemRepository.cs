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
    public sealed class CartRepository
    {
        public async Task<IReadOnlyList<OrderItemLine>> GetOrderItemLinesAsync(int orderId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<OrderItemLine>().Where(cl => cl.OrderId == orderId).ToListAsync(cancellationToken) ;
        }

        public async Task AddLineAsync(OrderItemLine line, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.InsertAsync(line);
        }

        public async Task RemoveLineAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<OrderItemLine>().Where(cl => cl.OrderId == orderId && cl.ProductId == productId).DeleteAsync(cancellationToken);        }

        public async Task AssignToOrderAsync(int orderId, int productId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<OrderItemLine>().Where(cl => cl.ProductId == productId).Set(cl => cl.OrderId, orderId).UpdateAsync(cancellationToken);
        }
    }
}