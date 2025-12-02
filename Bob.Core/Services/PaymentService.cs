using Bob.Core.Domain;
using Bob.Core.DTO;
using Bob.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class PaymentService
    {
        private readonly IPaymentRepository m_PaymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            m_PaymentRepository = paymentRepository;
        }

#nullable enable
        public async Task<PaymentDto?> GetPaymentByIdAsync(uint paymentId)
        {
            var payment = await m_PaymentRepository.GetByIdAsync(paymentId);
            if (payment == null) return null;
            return new PaymentDto(payment.Id, payment.OrderId, payment.PaymentDate, payment.ProcessorId);
        }

        public async Task<PaymentDto?> GetPaymentsForOrderAsync(uint orderId)
        {
            var payment = await m_PaymentRepository.GetByOrderIdAsync(orderId);
            if (payment == null) return null;
            return new PaymentDto(payment.Id, payment.OrderId, payment.PaymentDate, payment.ProcessorId);
        }

        public async Task ProcessPaymentAsync(PaymentDto payment)
        {
            var entity = new Payment
            (
                payment.PaymentId != 0 ? payment.PaymentId : 0,
                payment.OrderId,
                payment.PaymentDate,
                payment.ProcessorId
            );
            await m_PaymentRepository.AddAsync(entity);
        }
    }
}
