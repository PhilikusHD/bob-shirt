#nullable enable
using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class PaymentService
    {
        public static async Task<Payment?> GetPaymentByIdAsync(int paymentId)
        {
            return await PaymentRepository.GetByIdAsync(paymentId);
        }

        public static async Task<Payment?> GetPaymentsForOrderAsync(int orderId)
        {
            return await PaymentRepository.GetByOrderIdAsync(orderId);
        }

        public static async Task ProcessPaymentAsync(Payment payment)
        {
            await PaymentRepository.AddAsync(payment);
        }

        public static async Task DeletePaymentAsync(int id) => await PaymentRepository.DeleteAsync(id);
    }
}
