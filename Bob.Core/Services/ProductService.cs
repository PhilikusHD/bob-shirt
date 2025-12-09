#nullable enable
using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class ProductService
    {


        #region Product
        public static async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await ProductRepository.GetByIdAsync(productId);
        }

        public static async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await ProductRepository.GetAllAsync();
        }

        public static async Task AddProductAsync(Product product, ProductVariant? variants = null)
        {
            await ProductRepository.AddAsync(product, variants);
        }

        public static async Task UpdateProductAsync(Product product)
        {
            await ProductRepository.UpdateAsync(product);
        }

        public static async Task DeleteProductAsync(int productId)
        {
            await ProductRepository.DeleteAsync(productId);
        }
        #endregion

        #region Size
        public static async Task<Size?> GetSizeAsync(int sizeId) => await ProductRepository.GetSizeByIdAsync(sizeId);

        public static async Task<IReadOnlyList<Size>> GetAllSizesAsync() => await ProductRepository.GetAllSizesAsync();

        public static async Task<decimal> GetPriceAdjustedForSize(int sizeId, decimal price)
        {
            return price * await ProductRepository.GetSizeMultiplier(sizeId);
        }
        #endregion

        #region ProductType
        public static async Task<ProductType?> GetProductTypeAsync(int productId) => await ProductRepository.GetProductTypeByProductAsync(productId);

        public static async Task<ProductType?> GetProductTypeByIdAsync(int typeId) => await ProductRepository.GetProductTypeByIDAsync(typeId);

        public static async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync() => await ProductRepository.GetAllProductTypesAsync();

        public static async Task AddProductTypeAsync(ProductType productType) => await ProductRepository.AddProductTypeAsync(productType);

        public static async Task UpdateProductTypeAsync(ProductType productType) => await ProductRepository.UpdateProductTypeAsync(productType);

        public static async Task DeleteProductTypeAsync(int typeId) => await ProductRepository.DeleteProductTypeAsync(typeId);
        #endregion

        #region Colors
        // Define Interfaces for the Color Table
        public static async Task<Color?> GetColorAsync(int colorId) => await ProductRepository.GetColorByIdAsync(colorId);

        public static async Task<IReadOnlyList<Color>> GetAllColorsAsync() => await ProductRepository.GetAllColorsAsync();
        #endregion

        #region ProductVariant
        public static async Task<ProductVariant?> GetVariantAsync(int variantId)
        {
            return await ProductRepository.GetVariantByIdAsync(variantId);
        }

        public static async Task<IReadOnlyList<ProductVariant>> GetAllVariantsAsync()
        {
            return await ProductRepository.GetAllVariantsAsync();
        }

        public static async Task<IReadOnlyList<ProductVariant>> GetVariantsForProductAsync(int productId)
        {
            return await ProductRepository.GetVariantsForProductAsync(productId);
        }

        public static async Task AddVariantAsync(ProductVariant variant)
        {
            await ProductRepository.AddVariantAsync(variant);
        }

        public static async Task UpdateVariantAsync(ProductVariant variant)
        {
            await ProductRepository.UpdateVariantAsync(variant);
        }

        public static async Task DeleteVariantAsync(ProductVariant variant)
        {
            await ProductRepository.DeleteVariantAsync(variant);
        }
        #endregion
    }
}
