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
    public class AddressService
    {
        private readonly IAddressRepository m_AddressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            m_AddressRepository = addressRepository;
        }

#nullable enable
        public async Task<AddressDto?> GetAddressByIdAsync(uint addressId)
        {
            var address = await m_AddressRepository.GetByIdAsync(addressId);
            if (address == null) return null;
            return new AddressDto(address.Id, address.Street, address.HouseNumber, address.PostalCode, address.City);
        }

        public async Task<AddressDto?> GetAddressForCustomerAsync(uint customerId)
        {
            var address = await m_AddressRepository.GetByCustomerIdAsync(customerId);
            if (address == null) return null;
            return new AddressDto(address.Id, address.Street, address.HouseNumber, address.PostalCode, address.City);
        }

        public async Task AddAddressAsync(AddressDto address)
        {
            var entity = new Address(
                address.AddressId != 0 ? address.AddressId : 0,
                address.Street,
                address.HouseNumber,
                address.PostalCode,
                address.City
            );
            await m_AddressRepository.AddAsync(entity);
        }

        public async Task UpdateAddressAsync(AddressDto address)
        {
            var entity = new Address
            (
                address.AddressId,
                address.Street,
                address.HouseNumber,
                address.PostalCode,
                address.City
            );
            await m_AddressRepository.UpdateAsync(entity);
        }
    }
}
