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
        public async Task<IReadOnlyList<CartLine>> GetCartLinesAsync(CartId cartId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await GetCartLinesAsync(db, cartId, cancellationToken);
        }

        public async Task AddLineAsync(CartLine line, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await AddLineAsync(db, line, cancellationToken);
        }

        public async Task RemoveLineAsync(CartId cartId, ItemId itemId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await RemoveLineAsync(db, cartId, itemId, cancellationToken);
        }

        public async Task AssignToOrderAsync(CartId cartId, OrderId orderId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await AssignToOrderAsync(db, cartId, orderId, cancellationToken);
        }

        // Transaction-aware overloads
        public async Task<IReadOnlyList<CartLine>> GetCartLinesAsync(DataConnection connection, CartId cartId, CancellationToken cancellationToken = default)
        {
            return await connection.GetTable<CartLine>().Where(cl => cl.CartId == cartId).ToListAsync(cancellationToken);
        }

        public async Task AddLineAsync(DataConnection connection, CartLine line, CancellationToken cancellationToken = default)
        {
            await connection.InsertAsync(line);
        }

        public async Task RemoveLineAsync(DataConnection connection, CartId cartId, ItemId itemId, CancellationToken cancellationToken = default)
        {
            await connection.GetTable<CartLine>().Where(cl => cl.CartId == cartId && cl.ItemId == itemId).DeleteAsync(cancellationToken);
        }

        public async Task AssignToOrderAsync(DataConnection connection, CartId cartId, OrderId orderId, CancellationToken cancellationToken = default)
        {
            await connection.GetTable<CartLine>().Where(cl => cl.CartId == cartId).Set(cl => cl.OrderId, orderId).UpdateAsync(cancellationToken);
        }
    }
}
