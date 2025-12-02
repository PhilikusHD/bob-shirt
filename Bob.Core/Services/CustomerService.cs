using Bob.Core.Domain;
using Bob.Core.DTO;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository m_CustomerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            m_CustomerRepository = customerRepository;
        }

#nullable enable
        public async Task<CustomerDto?> GetCustomerByIdAsync(uint customerId)
        {
            var customer = await m_CustomerRepository.GetByIdAsync(customerId);
            if (customer == null) return null;
            return new CustomerDto(
                customer.Id,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.AddressId,
                customer.PhoneNumber,
                customer.SignupDate
            );
        }

        public async Task<IReadOnlyList<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await m_CustomerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto(
                c.Id, c.Name, c.Surname, c.Email, c.AddressId, c.PhoneNumber, c.SignupDate
            )).ToList();
        }

        public async Task AddCustomerAsync(CustomerDto customer)
        {
            var entity = new Customer(
                customer.CustomerId != 0 ? customer.CustomerId : 0,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.AddressId,
                customer.PhoneNumber,
                customer.SignupDate
            );
            await m_CustomerRepository.AddAsync(entity);
        }

        public async Task UpdateCustomerAsync(CustomerDto customer)
        {
            var entity = new Customer(
                customer.CustomerId,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.AddressId,
                customer.PhoneNumber,
                customer.SignupDate
            );
            await m_CustomerRepository.UpdateAsync(entity);
        }
    }
}
