using Bob.Core.Domain;
using Bob.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.CartSytem
{
    public struct CartState
    {
        public IReadOnlyList<ProductVariant> ProductVariants;
        public decimal TotalPrice;
    }


    public sealed class CartSystem
    {
        private readonly ProductService m_ProductService;
        private readonly OrderItemService m_ItemService;

        private List<ProductVariant> m_ProductVariants = new List<ProductVariant>();

        private decimal m_TotalPrice;

        public CartSystem(ProductService productService, OrderItemService itemService)
        {
            m_ProductService = productService;
            m_ItemService = itemService;
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

        public async Task CalculateTotalPrice()
        {
            m_TotalPrice = 0.0M;
            IReadOnlyList<Product> products = await m_ProductService.GetAllProductsAsync();
            IReadOnlyList<Size> allSizes = await m_ProductService.GetAllSizesAsync();

            Dictionary<int, decimal> sizeMultipliers = new();
            Dictionary<int, decimal> productPrices = new();

            foreach (Size size in allSizes)
                sizeMultipliers.TryAdd(size.SizeId, size.PriceMultiplier);

            foreach (Product product in products)
                productPrices.TryAdd(product.ProductId, product.Price);

            foreach (var variant in m_ProductVariants)
                m_TotalPrice += productPrices[variant.ProductId] * sizeMultipliers[variant.SizeId];
        }

        public async Task CreateOrderDraft(Order order)
        {

            Dictionary<int, int> variantQuantityMap = new();
            for (int i = 0; i < m_ProductVariants.Count; i++)
            {
                int id = m_ProductVariants[i].VariantId;
                if (variantQuantityMap.ContainsKey(id))
                    variantQuantityMap[id] += 1;
                else
                    variantQuantityMap.Add(id, 1);
            }

            foreach (var variant in variantQuantityMap)
            {
                OrderItemLine orderItemLine = new OrderItemLine
                {
                    VariantId = variant.Key,
                    Amount = variant.Value,
                    OrderId = order.Id
                };

                await m_ItemService.AddLineAsync(orderItemLine);
            }
        }
    }
}
