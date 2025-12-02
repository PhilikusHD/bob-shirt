using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
    public interface ICartRepository
    {
        Task<IReadOnlyList<CartLine>> GetCartLinesAsync(CartId cartId, CancellationToken cancellationToken = default);
        Task AddLineAsync(CartLine line, CancellationToken cancellationToken = default);
        Task RemoveLineAsync(CartId cartId, ItemId itemId, CancellationToken cancellationToken = default);
        Task AssignToOrderAsync(CartId cartId, OrderId orderId, CancellationToken cancellationToken = default);
    }
}
