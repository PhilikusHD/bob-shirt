using Bob.Core.DTO;
using Bob.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class PaymentProcessorService
    {
        private readonly IPaymentProcessorRepository m_ProcessorRepository;

        public PaymentProcessorService(IPaymentProcessorRepository processorRepository)
        {
            m_ProcessorRepository = processorRepository;
        }

#nullable enable
        public async Task<PaymentProcessorDto?> GetProcessorByIdAsync(uint processorId)
        {
            var processor = await m_ProcessorRepository.GetByIdAsync(processorId);
            if (processor == null) return null;
            return new PaymentProcessorDto(processor.Id, processor.Method);
        }

        public async Task<IReadOnlyList<PaymentProcessorDto>> GetAllProcessorsAsync()
        {
            var processors = await m_ProcessorRepository.GetAllAsync();
            return processors.Select(p => new PaymentProcessorDto(p.Id, p.Method)).ToList();
        }
    }
}
