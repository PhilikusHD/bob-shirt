using System;
using Bob.Core.Domain;
using Bob.Core.Database;
using LinqToDB;
using LinqToDB.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bob.Core.Logging;

namespace Bob.Core.Repositories
{
#nullable enable
    public sealed class ProductRepository
    {
        // PRODUCT Table
        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Product>().FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Product>().ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Product product, ProductVariant? variant = null, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await using var tran = await db.BeginTransactionAsync(cancellationToken);

            try
            {
                var identity = await db.InsertAsync(product);
                var id = Convert.ToInt32(identity);
                product.Id = (int)id;


                if (variant != null)
                {
                    variant.ProductId = product.Id;
                    await db.InsertAsync(variant);
                }

                await tran.CommitAsync(cancellationToken);
            }
            catch
            {
                await tran.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(product);
        }

        public async Task DeleteAsync(int productId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await using var tran = await db.BeginTransactionAsync(cancellationToken);

            try
            {
                var variantsDeleted = await db.GetTable<ProductVariant>()
                                              .Where(v => v.ProductId == productId)
                                              .DeleteAsync(cancellationToken);

                if (variantsDeleted > 0)
                    Logger.Debug($"Deleted {variantsDeleted} variants");

                var productDeleted = await db.GetTable<Product>()
                                             .Where(p => p.Id == productId)
                                             .DeleteAsync(cancellationToken);

                Logger.Debug(productDeleted > 0 ? "Deleted product" : "Product not found");

                await tran.CommitAsync(cancellationToken);
            }
            catch
            {
                await tran.RollbackAsync(cancellationToken);
                throw;
            }
        }


        public async Task<decimal> GetSizeMultiplier(int sizeId)
        {
            await using var db = new AppDataConnection();
            var size = await db.GetTable<Size>().FirstOrDefaultAsync(s => s.SizeId == sizeId);

            return size != null ? size.PriceMultiplier : 1;
        }

        // ProductVariant specific

        public async Task<ProductVariant?> GetVariantByIdAsync(int variantId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<ProductVariant>().FirstOrDefaultAsync(v => v.VariantId == variantId, cancellationToken);
        }

        public async Task<IReadOnlyList<ProductVariant>> GetVariantsForProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<ProductVariant>().Where(v => v.ProductId == productId).ToListAsync(cancellationToken);
        }

        public async Task AddVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.InsertAsync(variant);
        }

        public async Task UpdateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(variant);
        }

        public async Task DeleteVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<ProductVariant>()
                .Where(v => v.VariantId == variant.VariantId && v.ProductId == variant.ProductId && v.ColorId == variant.ColorId && v.SizeId == variant.SizeId)
                .DeleteAsync(cancellationToken);
        }
    }
}
