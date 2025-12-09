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

    public static class CartSystem
    {
        private static readonly List<ProductVariant> m_ProductVariants = [];

        private static decimal m_TotalPrice;

        public static async Task AddToCart(ProductVariant productVariant)
        {
            m_ProductVariants.Add(productVariant);
            await CalculateTotalPrice();
        }

        public static async Task RemoveFromCart(ProductVariant productVariant)
        {
            m_ProductVariants.Remove(productVariant);
            await CalculateTotalPrice();
        }

        public static void Clear()
        {
            m_ProductVariants.Clear();
            m_TotalPrice = 0.0M;
        }

        public static CartState GetCartState()
        {
            return new CartState
            {
                ProductVariants = m_ProductVariants.AsReadOnly(),
                TotalPrice = m_TotalPrice
            };
        }

        public static int GetItemCount()
        {
            return m_ProductVariants.Count;
        }

        public static async Task CalculateTotalPrice()
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

        public static async Task<Order> CreateOrderDraft(int customerId)
        {

            Dictionary<int, int> variantQuantityMap = [];
            for (int i = 0; i < m_ProductVariants.Count; i++)
            {
                int id = m_ProductVariants[i].VariantId;
                if (!variantQuantityMap.TryAdd(id, 1))
                    variantQuantityMap[id] += 1;
            }

            Order order = new Order();
            int orderId = await OrderService.GetHighestId() + 1;
            order.Id = orderId;
            order.CustomerId = customerId;

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

            return order;
        }
    }
}
