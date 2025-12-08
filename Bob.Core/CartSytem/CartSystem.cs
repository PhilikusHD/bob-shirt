using Bob.Core.Domain;
using Bob.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.CartSytem
{
    public struct CartState
    {
        public List<ProductVariant> ProductVariants;
        public decimal TotalPrice;
    }


    public sealed class CartSystem
    {
        private readonly ProductService m_ProductService;
        private readonly OrderItemService m_ItemService;

        private List<ProductVariant> m_ProductVariants = new List<ProductVariant>();

        private decimal m_TotalPrice { get; set; }

        public CartSystem(ProductService productService, OrderItemService itemService)
        {
            m_ProductService = productService;
            m_ItemService = itemService;
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

        public CartState GetCartState()
        {
            return new CartState
            {
                ProductVariants = m_ProductVariants,
                TotalPrice = m_TotalPrice
            };
        }

        public async Task<decimal> CalculateTotalPrice()
        {
            foreach (var variant in m_ProductVariants)
            {
                var product = await m_ProductService.GetProductByIdAsync(variant.ProductId);
                m_TotalPrice += await m_ProductService.GetPriceAdjustedForSize(variant.SizeId, product.Price);
            }

            return m_TotalPrice;
        }

        public async Task CreateOrderDraft(Order order)
        {

            List<KeyValuePair<ProductVariant, int>> variantCounts = new List<KeyValuePair<ProductVariant, int>>();
            for (int i = 0; i < m_ProductVariants.Count; i++)
            {
                var variant = m_ProductVariants[i];
                var existingEntry = variantCounts.FindIndex(v => v.Key.VariantId == variant.VariantId);
                if (existingEntry >= 0)
                {
                    variantCounts[existingEntry] = new KeyValuePair<ProductVariant, int>(variant, variantCounts[existingEntry].Value + 1);
                }
                else
                {
                    variantCounts.Add(new KeyValuePair<ProductVariant, int>(variant, 1));
                }
            }

            foreach (var variant in variantCounts)
            {
                OrderItemLine orderItemLine = new OrderItemLine
                {
                    VariantId = variant.Key.VariantId,
                    Amount = variant.Value,
                    OrderId = order.Id
                };

                await m_ItemService.AddLineAsync(orderItemLine);
            }
        }
    }
}
