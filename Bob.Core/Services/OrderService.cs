using Bob.Core.Repositories;
using Bob.Core.Domain;
using Bob.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Bob.Core.Services
{
#nullable enable
    public static class OrderService
    {
        public static async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await OrderRepository.GetByIdAsync(orderId);
        }

        public static async Task<IReadOnlyList<Order>> GetOrderForCustomerAsync(int customerId)
        {
            return await OrderRepository.GetByCustomerAsync(customerId);
        }

        public static async Task<int> GetHighestId() => await OrderRepository.GetHighestId();

        public static async Task<int> CreateOrderAsync(Order order)
        {
            var lines = await OrderItemService.GetOrderItemLinesAsync(order.Id);
            if (lines == null || lines.Count == 0)
                return -1;

            decimal totalAmount = 0;
            foreach (var line in lines)
            {
                var variant = await ProductService.GetVariantAsync(line.VariantId);
                if (variant == null)
                {
                    Logger.Warning($"Variant with ID {line.VariantId} does not exist. Skipping...");
                    continue;
                }

                var item = await ProductService.GetProductByIdAsync(variant.ProductId);

                if (item != null)
                    totalAmount += await ProductService.GetPriceAdjustedForSize(variant.SizeId, item.Price);
            }

            order.TotalAmount = totalAmount;
            order.OrderDate = DateTime.UtcNow;

            await OrderRepository.AddAsync(order);
            return order.Id;
        }

        public static async Task CancelOrderAsync(int orderId)
        {
            var order = await OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                Logger.Error("Order not found");
                return;
            }

            await OrderRepository.DeleteAsync(orderId);
        }
    }
}