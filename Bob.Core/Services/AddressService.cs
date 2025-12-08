#nullable enable
using Bob.Core.Domain;
using Bob.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class AddressService
    {

        public static async Task<Address?> GetAddressByIdAsync(int addressId)
        {
            return await AddressRepository.GetByIdAsync(addressId);
        }

        public static async Task<Tuple<bool, int>> AddressExists(Address address)
        {
            return await AddressRepository.AddressExists(address);
        }

        public static async Task<Address?> GetAddressForCustomerAsync(uint customerId)
        {
            return await AddressRepository.GetByCustomerIdAsync(customerId);
        }

        public static async Task AddAddressAsync(Address address)
        {
            await AddressRepository.AddAsync(address);
        }

        public static async Task UpdateAddressAsync(Address address)
        {
            await AddressRepository.UpdateAsync(address);
        }

        public static async Task DeleteAddressAsync(int id) => await AddressRepository.DeleteAsync(id);
    }
}
