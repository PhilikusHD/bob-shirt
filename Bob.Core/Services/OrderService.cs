using Bob.Core.Repositories;
using Bob.Core.Domain;
using Bob.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
#nullable enable
    public class OrderService
    {
        private readonly OrderRepository m_OrderRepository;
        private readonly CartService m_CartService;
        private readonly ItemService m_ItemService;

        public OrderService(OrderRepository orderRepository, CartService cartService, ItemService itemService)
        {
            m_OrderRepository = orderRepository;
            m_CartService = cartService;
            m_ItemService = itemService;
        }

        public async Task<Order?> GetOrderByIdAsync(OrderId orderId)
        {
            return await m_OrderRepository.GetByIdAsync(orderId);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForCustomerAsync(CustomerId customerId)
        {
            return await m_OrderRepository.GetByCustomerAsync(customerId);
        }

        public async Task<OrderId> CreateOrderAsync(Order order)
        {
            var carts = await m_CartService.GetCartLinesAsync(order.CartId);
            if (carts == null)
                return (OrderId)(-1);

            decimal totalAmount = 0;
            foreach (var cart in carts)
            {
                var item = await m_ItemService.GetItemByIdAsync(cart.ItemId);
                if (item != null)
                    totalAmount += item.Price;
            }

            order.TotalAmount = totalAmount;
            order.OrderDate = DateTime.UtcNow;

            await m_OrderRepository.AddAsync(order);
            return order.Id;
        }

        public async Task CancelOrderAsync(OrderId orderId)
        {
            var order = await m_OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                Logger.Error("Order not found");
                return;
            }

            await m_OrderRepository.DeleteAsync(orderId);
        }
    }
}