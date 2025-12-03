using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class PaymentService
    {
        private readonly PaymentRepository m_PaymentRepository;

        public PaymentService(PaymentRepository paymentRepository)
        {
            m_PaymentRepository = paymentRepository;
        }

#nullable enable
        public async Task<Payment?> GetPaymentByIdAsync(PaymentId paymentId)
        {
            return await m_PaymentRepository.GetByIdAsync(paymentId);
        }

        public async Task<Payment?> GetPaymentsForOrderAsync(OrderId orderId)
        {
            return await m_PaymentRepository.GetByOrderIdAsync(orderId);
        }

        public async Task ProcessPaymentAsync(Payment payment)
        {
            await m_PaymentRepository.AddAsync(payment);
        }
    }
}
