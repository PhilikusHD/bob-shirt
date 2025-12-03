using Bob.Core.Repositories;
using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class PaymentProcessorService
    {
        private readonly PaymentProcessorRepository m_ProcessorRepository;

        public PaymentProcessorService(PaymentProcessorRepository processorRepository)
        {
            m_ProcessorRepository = processorRepository;
        }

#nullable enable
        public async Task<PaymentProcessor?> GetProcessorByIdAsync(ProcessorId processorId)
        {
            return await m_ProcessorRepository.GetByIdAsync(processorId);
        }

        public async Task<IReadOnlyList<PaymentProcessor>> GetAllProcessorsAsync()
        {
            return await m_ProcessorRepository.GetAllAsync();
        }
    }
}
