using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
#nullable enable
    public class ProductService
    {
        private readonly ProductRepository m_ProductRepository;

        public ProductService(ProductRepository repository)
        {
            m_ProductRepository = repository;
        }

        // Product
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await m_ProductRepository.GetByIdAsync(productId);
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await m_ProductRepository.GetAllAsync();
        }

        public async Task AddProductAsync(Product product, ProductVariant? variants = null)
        {
            await m_ProductRepository.AddAsync(product, variants);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await m_ProductRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await m_ProductRepository.DeleteAsync(productId);
        }


        public async Task<decimal> GetPriceAdjustedForSize(int sizeId, decimal price)
        {
            return price * await m_ProductRepository.GetSizeMultiplier(sizeId);
        }


        // ProductVariant convenience

        public async Task<ProductVariant?> GetVariantAsync(int variantId)
        {
            return await m_ProductRepository.GetVariantByIdAsync(variantId);
        }

        public async Task<IReadOnlyList<ProductVariant>> GetVariantsForProductAsync(int productId)
        {
            return await m_ProductRepository.GetVariantsForProductAsync(productId);
        }

        public async Task AddVariantAsync(ProductVariant variant)
        {
            await m_ProductRepository.AddVariantAsync(variant);
        }

        public async Task UpdateVariantAsync(ProductVariant variant)
        {
            await m_ProductRepository.UpdateVariantAsync(variant);
        }

        public async Task DeleteVariantAsync(ProductVariant variant)
        {
            await m_ProductRepository.DeleteVariantAsync(variant);
        }
    }
}
