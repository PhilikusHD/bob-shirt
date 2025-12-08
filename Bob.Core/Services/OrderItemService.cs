using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public static class OrderItemService
    {
        public static async Task<IReadOnlyList<OrderItemLine>> GetOrderItemLinesAsync(int orderItemId)
        {
            return await OrderItemRepository.GetOrderItemLinesAsync(orderItemId);
        }

        public static async Task AddLineAsync(OrderItemLine line)
        {
            var variant = await ProductService.GetVariantAsync(line.VariantId);
            if (variant == null)
            {
                Logger.Error("Variant does not exist.");
                return;
            }

            await OrderItemRepository.AddLineAsync(line);
        }

        public static async Task RemoveLineAsync(int orderId, int itemId) => await OrderItemRepository.RemoveLineAsync(orderId, itemId);

        public static async Task AssignToOrderAsync(int orderId, int productId) => await OrderItemRepository.AssignToOrderAsync(orderId, productId);
    }
}
