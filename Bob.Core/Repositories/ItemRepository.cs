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
    public sealed class ItemRepository
    {
        public async Task<Item?> GetByIdAsync(ItemId id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Item>().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Item>().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Item item, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var identity = await db.InsertWithIdentityAsync(item);
            if (identity != null)
            {
                // Try to set the generated id if possible
                try
                {
                    var id = Convert.ToInt32(identity);
                    item.Id = (ItemId)id;
                }
                catch { }
            }
        }

        public async Task UpdateAsync(Item item, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(item);
        }

        public async Task DeleteAsync(ItemId id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<Item>().Where(i => i.Id == id).DeleteAsync(cancellationToken);
        }
    }
}
