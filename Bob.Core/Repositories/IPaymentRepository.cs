using Bob.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public interface IPaymentRepository
    {
        Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default);
        Task<Payment?> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);

        Task AddAsync(Payment payment, CancellationToken cancellationToken = default);
    }
}
