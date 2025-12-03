using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository m_CustomerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            m_CustomerRepository = customerRepository;
        }

#nullable enable
        public async Task<Customer?> GetCustomerByIdAsync(int customerId)
        {
            return await m_CustomerRepository.GetByIdAsync(customerId);
        }

        public async Task<IReadOnlyList<Customer>> GetAllCustomersAsync()
        {
            return await m_CustomerRepository.GetAllAsync();
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await m_CustomerRepository.AddAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await m_CustomerRepository.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id) => await m_CustomerRepository.DeleteAsync(id);
    }
}
