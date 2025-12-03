using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class AddressService
    {
        private readonly AddressRepository m_AddressRepository;

        public AddressService(AddressRepository addressRepository)
        {
            m_AddressRepository = addressRepository;
        }

#nullable enable
        public async Task<Address?> GetAddressByIdAsync(int addressId)
        {
            return await m_AddressRepository.GetByIdAsync(addressId);
        }

        public async Task<Address?> GetAddressForCustomerAsync(uint customerId)
        {
            return await m_AddressRepository.GetByCustomerIdAsync(customerId);
        }

        public async Task AddAddressAsync(Address address)
        {
            await m_AddressRepository.AddAsync(address);
        }

        public async Task UpdateAddressAsync(Address address)
        {
            await m_AddressRepository.UpdateAsync(address);
        }

        public async Task DeleteAddressAsync(int id) => await m_AddressRepository.DeleteAsync(id);
    }
}
