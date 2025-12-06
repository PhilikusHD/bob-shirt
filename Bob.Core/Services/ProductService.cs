using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        #region Product
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
        #endregion

        #region Size
        public async Task<Size?> GetSizeAsync(int sizeId) => await m_ProductRepository.GetSizeByIdAsync(sizeId);

        public async Task<IReadOnlyList<Size>> GetAllSizesAsync() => await m_ProductRepository.GetAllSizesAsync();

        public async Task<decimal> GetPriceAdjustedForSize(int sizeId, decimal price)
        {
            return price * await m_ProductRepository.GetSizeMultiplier(sizeId);
        }
        #endregion

        #region ProductType
        public async Task<ProductType?> GetProductTypeAsync(int productId) => await m_ProductRepository.GetProductTypeByProductAsync(productId);

        public async Task<ProductType?> GetProductTypeByIdAsync(int typeId) => await m_ProductRepository.GetProductTypeByIDAsync(typeId);

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync() => await m_ProductRepository.GetAllProductTypesAsync();

        public async Task AddProductTypeAsync(ProductType productType) => await m_ProductRepository.AddProductTypeAsync(productType);

        public async Task UpdateProductTypeAsync(ProductType productType) => await m_ProductRepository.UpdateProductTypeAsync(productType);

        public async Task DeleteProductTypeAsync(int typeId) => await m_ProductRepository.DeleteProductTypeAsync(typeId);
        #endregion

        #region Colors
        // Define Interfaces for the Color Table

        public async Task<Color?> GetColorAsync(int colorId) => await m_ProductRepository.GetColorByIdAsync(colorId);

        public async Task<IReadOnlyList<Color>> GetAllColorsAsync() => await m_ProductRepository.GetAllColorsAsync();
        #endregion

        #region ProductVariant
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
        #endregion
    }
}
