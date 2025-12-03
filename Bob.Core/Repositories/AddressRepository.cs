using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;

namespace Bob.Core.Repositories
{
#nullable enable
    public sealed class AddressRepository
    {
        public async Task<Address?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Address>().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Address?> GetByCustomerIdAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Address>()
                .Join(db.GetTable<Customer>(),
                      a => a.Id,
                      c => c.AddressId,
                      (a, c) => a)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddAsync(Address address, CancellationToken cancellationToken = default)
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

        public async Task UpdateAsync(Address address, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(address);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Address>().Where(i => i.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
