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

        public static async Task<bool> AddressExists(Address address)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Address>()
                .AnyAsync(a => a.Street == address.Street &&
                               a.HouseNumber == address.HouseNumber &&
                               a.PostalCode == address.PostalCode &&
                               a.City == address.City);
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
            var identity = await db.InsertWithIdentityAsync(address);
            if (identity != null)
            {
                try
                {
                    address.Id = Convert.ToInt32(identity);
                }
                catch { }
            }
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
