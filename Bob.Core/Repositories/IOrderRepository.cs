using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Order>> GetByCustomerAsync(CustomerId customerId, CancellationToken cancellationToken = default);

        Task AddAsync(Order order, CancellationToken cancellationToken = default);
        Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    }
}
