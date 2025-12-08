#nullable enable
using Bob.Core.Repositories;
using Bob.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class PaymentProcessorService
    {
        public static async Task<PaymentProcessor?> GetProcessorByIdAsync(int processorId)
        {
            return await PaymentProcessorRepository.GetByIdAsync(processorId);
        }

        public static async Task<IReadOnlyList<PaymentProcessor>> GetAllProcessorsAsync()
        {
            return await PaymentProcessorRepository.GetAllAsync();
        }
    }
}
