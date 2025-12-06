#nullable enable

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
    public sealed class ProductRepository
    {
        // PRODUCT Table
        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Product>().FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
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
                await db.InsertAsync(product, token: cancellationToken);


                if (variant != null)
                {
                    variant.ProductId = product.ProductId;
                    await db.InsertAsync(variant, token: cancellationToken);
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
            await db.UpdateAsync(product, token: cancellationToken);
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
                                             .Where(p => p.ProductId == productId)
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

        // ProductType

        public async Task<ProductType?> GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            var product = await db.GetTable<Product>().FirstOrDefaultAsync(p => p.ProductId == productId, token: cancellationToken);
            if (product == null)
            {
                Logger.Warning($"Product with ID {productId} does not exist.");
                return new ProductType();
            }

            return await db.GetTable<ProductType>().FirstOrDefaultAsync(pt => pt.TypeId == product.TypeId, token: cancellationToken);
        }

        public async Task<ProductType?> GetProductTypeByIDAsync(int typeId, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<ProductType>().FirstOrDefaultAsync(pt => pt.TypeId == typeId, token: cancellationToken);
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<ProductType>().ToListAsync();
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
            await using var db = new AppDataConnection();
            try
            {
                await db.InsertAsync(productType);
            }
            catch (Exception ex)
            {
                Logger.Warning($"Could not add productType to database. Reason: {ex.Message}");
            }
        }

        public async Task UpdateProductTypeAsync(ProductType productType)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(productType);
        }

        public async Task DeleteProductTypeAsync(int typeId)
        {
            await using var db = new AppDataConnection();
            await db.GetTable<ProductType>().Where(t => t.TypeId == typeId).DeleteAsync();
        }

        // Size
        public async Task<Size?> GetSizeByIdAsync(int sizeId)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Size>().FirstOrDefaultAsync(s => s.SizeId == sizeId);
        }

        public async Task<IReadOnlyList<Size>> GetAllSizesAsync()
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Size>().ToListAsync();
        }

        public async Task<decimal> GetSizeMultiplier(int sizeId)
        {
            await using var db = new AppDataConnection();
            var size = await db.GetTable<Size>().FirstOrDefaultAsync(s => s.SizeId == sizeId);

            return size != null ? size.PriceMultiplier : 1;
        }


        // Color
        public async Task<Color?> GetColorByIdAsync(int colorId)
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Color>().FirstOrDefaultAsync(s => s.Id == colorId);
        }

        public async Task<IReadOnlyList<Color>> GetAllColorsAsync()
        {
            await using var db = new AppDataConnection();
            return await db.GetTable<Color>().ToListAsync();
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
            await db.InsertAsync(variant, token: cancellationToken);
        }

        public async Task UpdateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
        {
            await using var db = new AppDataConnection();
            await db.UpdateAsync(variant, token: cancellationToken);
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
