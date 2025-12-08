using Bob.Core.Domain;
using Bob.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.CartSytem
{
    public sealed class CartSystem
    {
        private readonly ProductService m_ProductService;

        private List<ProductVariant> m_ProductVariants = new List<ProductVariant>();

        private decimal m_TotalPrice { get; set; }

        public CartSystem(ProductService productService)
        {
            m_ProductService = productService;
        }

        public async Task AddToCart(ProductVariant productVariant)
        {
            m_ProductVariants.Add(productVariant);
            var product = await m_ProductService.GetProductByIdAsync(productVariant.ProductId);

            m_TotalPrice += await m_ProductService.GetPriceAdjustedForSize(productVariant.SizeId, product.Price);
        }

        public async Task RemoveFromCart(ProductVariant productVariant)
        {
            m_ProductVariants.Remove(productVariant);

            var product = await m_ProductService.GetProductByIdAsync(productVariant.ProductId);
            m_TotalPrice -= await m_ProductService.GetPriceAdjustedForSize(productVariant.SizeId, product.Price);
        }

        public void Clear()
        {
            m_ProductVariants.Clear();
            m_TotalPrice = 0.0M;
        }
    }
}
