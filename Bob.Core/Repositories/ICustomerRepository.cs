using Bob.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(CustomerId id, CancellationToken cancellationToken = default);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    }
}
