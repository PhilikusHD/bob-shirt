using Bob.Core.Database;
using Bob.Core.Domain;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public static class CustomerRepository
    {
        public static async Task<Customer?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Customer>().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public static async Task<int> GetHighestId()
        {
            await using var db = new AppDataConnection();
            var highestId = await db.GetTable<Customer>().MaxAsync(c => (int?)c.Id);
            return highestId ?? 0;
        }

        public static async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Customer>().ToListAsync(cancellationToken);
        }

        public static async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Customer>().FirstOrDefaultAsync(c => c.Email == email, cancellationToken);
        }

        public static async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.InsertAsync(customer);
        }

        public static async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(customer);
        }

        public static async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Customer>().Where(i => i.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
