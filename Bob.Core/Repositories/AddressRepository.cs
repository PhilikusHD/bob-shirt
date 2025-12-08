using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using System.Net.NetworkInformation;
using Avalonia.Data.Converters;

namespace Bob.Core.Repositories
{
#nullable enable
    public static class AddressRepository
    {
        public static async Task<Address?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Address>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public static async Task<int?> GetHighestId(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var highestId = await db.GetTable<Address>().MaxAsync(c => (int?)c.Id);
            return highestId;
        }

        public static async Task<int> AddressExists(Address address)
        {
            await using var db = new AppDataConnection();

            var existingAddress = await db.GetTable<Address>()
                .FirstOrDefaultAsync(a =>
                    a.Street == address.Street &&
                    a.HouseNumber == address.HouseNumber &&
                    a.PostalCode == address.PostalCode &&
                    a.City == address.City);

            if (existingAddress != null)
                return existingAddress.Id;

            int? highestId = await GetHighestId();
            return highestId.HasValue ? highestId.Value + 1 : 1;
        }

        public static async Task<Address?> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Address>()
                .Join(db.GetTable<Customer>(),
                      a => a.Id,
                      c => c.AddressId,
                      (a, c) => a)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public static async Task AddAsync(Address address, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var identity = await db.InsertAsync(address);
        }

        public static async Task UpdateAsync(Address address, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(address);
        }

        public static async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Address>().Where(i => i.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
