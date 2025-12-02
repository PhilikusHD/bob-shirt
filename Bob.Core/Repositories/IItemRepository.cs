using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
    public interface IItemRepository
    {
        Task<Item> GetByIdAsync(ItemId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default);

        Task AddAsync(Item item, CancellationToken cancellationToken = default);
        Task UpdateAsync(Item item, CancellationToken cancellationToken = default);
        Task DeleteAsync(ItemId id, CancellationToken cancellationToken = default);
    }
}
