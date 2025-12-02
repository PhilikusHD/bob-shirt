using Bob.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
    internal interface IAddressRepository
    {
        Task<Address> GetByIdAsync(AddressId id, CancellationToken cancellationToken = default);

        Task AddAsync(Address address, CancellationToken cancellationToken = default);
        Task UpdateAsync(Address address, CancellationToken cancellationToken = default);
    }
}
