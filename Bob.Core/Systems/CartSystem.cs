using Bob.Core.Domain;
using Bob.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Systems
{
    public struct CartState
    {
        public IReadOnlyList<ProductVariant> ProductVariants;
        public decimal TotalPrice;
    }

    public sealed class CartSystem
    {
        private readonly List<ProductVariant> m_ProductVariants = [];

        private decimal m_TotalPrice;

        public CartSystem()
        {

        }

        public async Task AddToCart(ProductVariant productVariant)
        {
            m_ProductVariants.Add(productVariant);
            await CalculateTotalPrice();
        }

        public async Task RemoveFromCart(ProductVariant productVariant)
        {
            m_ProductVariants.Remove(productVariant);
            await CalculateTotalPrice();
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
                ProductVariants = m_ProductVariants.AsReadOnly(),
                TotalPrice = m_TotalPrice
            };
        }

        public int GetItemCount()
        {
            return m_ProductVariants.Count;
        }

        public async Task CalculateTotalPrice()
        {
            m_TotalPrice = 0.0M;
            IReadOnlyList<Product> products = await ProductService.GetAllProductsAsync();
            IReadOnlyList<Size> allSizes = await ProductService.GetAllSizesAsync();

            Dictionary<int, decimal> sizeMultipliers = [];
            Dictionary<int, decimal> productPrices = [];

            foreach (Size size in allSizes)
                sizeMultipliers.TryAdd(size.SizeId, size.PriceMultiplier);

            foreach (Product product in products)
                productPrices.TryAdd(product.ProductId, product.Price);

            foreach (var variant in m_ProductVariants)
                m_TotalPrice += productPrices[variant.ProductId] * sizeMultipliers[variant.SizeId];
        }

        public async Task CreateOrderDraft(Order order)
        {

            Dictionary<int, int> variantQuantityMap = [];
            for (int i = 0; i < m_ProductVariants.Count; i++)
            {
                int id = m_ProductVariants[i].VariantId;
                if (!variantQuantityMap.TryAdd(id, 1))
                    variantQuantityMap[id] += 1;
            }

            foreach (var variant in variantQuantityMap)
            {
                OrderItemLine orderItemLine = new()
                {
                    VariantId = variant.Key,
                    Amount = variant.Value,
                    OrderId = order.Id
                };

                await OrderItemService.AddLineAsync(orderItemLine);
            }
        }
    }
}
