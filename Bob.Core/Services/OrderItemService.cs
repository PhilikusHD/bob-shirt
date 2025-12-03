using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class OrderItemService
    {
        private readonly OrderItemRepository m_OrderItemRepository;
        private readonly ProductService m_ProductService;

        public OrderItemService(OrderItemRepository orderItemRepository, ProductService productService)
        {
            m_OrderItemRepository = orderItemRepository;
            m_ProductService = productService;
        }

        public async Task<IReadOnlyList<OrderItemLine>> GetOrderItemLinesAsync(int orderItemId)
        {
            return await m_OrderItemRepository.GetOrderItemLinesAsync(orderItemId);
        }

        public async Task AddLineAsync(OrderItemLine line)
        {
            var variant = await m_ProductService.GetVariantAsync(line.VariantId);
            if (variant == null)
            {
                Logger.Error("Variant does not exist.");
                return;
            }

            await m_OrderItemRepository.AddLineAsync(line);
        }

        public async Task RemoveLineAsync(int orderId, int itemId) => await m_OrderItemRepository.RemoveLineAsync(orderId, itemId);

        public async Task AssignToOrderAsync(int orderId, int productId) => await m_OrderItemRepository.AssignToOrderAsync(orderId, productId);
    }
}
