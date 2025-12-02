using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public interface IPaymentProcessorRepository
    {
        Task<PaymentProcessor?> GetByIdAsync(ProcessorId id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PaymentProcessor>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}