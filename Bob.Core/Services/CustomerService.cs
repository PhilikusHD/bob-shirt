#nullable enable
using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class CustomerService
    {
        public static async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await CustomerRepository.GetByIdAsync(customerId);
        }

        public static async Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
        {
            return await CustomerRepository.GetAllAsync();
        }

        public static async Task<int> GetHighestId()
        {
            return await CustomerRepository.GetHighestId();
        }

        public static async Task AddCustomerAsync(Customer customer)
        {
            await CustomerRepository.AddAsync(customer);
        }

        public static async Task UpdateCustomerAsync(Customer customer)
        {
            await CustomerRepository.UpdateAsync(customer);
        }

        public static async Task DeleteCustomerAsync(int id) => await CustomerRepository.DeleteAsync(id);
    }
}
