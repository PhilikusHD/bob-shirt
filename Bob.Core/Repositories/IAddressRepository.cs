using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> GetByIdAsync(AddressId id, CancellationToken cancellationToken = default);
        Task<Address> GetByCustomerIdAsync(uint customerId);

        Task AddAsync(Address address, CancellationToken cancellationToken = default);
        Task UpdateAsync(Address address, CancellationToken cancellationToken = default);
    }
}
