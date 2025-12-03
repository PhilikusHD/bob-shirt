using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Repositories
{
#nullable enable
    public sealed class ProductRepository
    {
        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Product>().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Product>().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(int product, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var identity = await db.InsertWithIdentityAsync(product);
            if (identity != null)
            {
                // Try to set the generated id if possible
                try
                {
                    var id = Convert.ToInt32(identity);
                    product.Id = (int)id;
                }
                catch { }
            }
        }

        public async Task UpdateAsync(int product, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(Product);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Product>().Where(i => i.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
