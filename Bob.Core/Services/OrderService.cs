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
        private readonly OrderItemService m_OrderItemService;
        private readonly ProductService m_ProductService;

        public OrderService(OrderRepository orderRepository, OrderItemService cartService, ProductService productService)
        {
            m_OrderRepository = orderRepository;
            m_OrderItemService = cartService;
            m_ProductService = productService;
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await m_OrderRepository.GetByIdAsync(orderId);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForCustomerAsync(int customerId)
        {
            return await m_OrderRepository.GetByCustomerAsync(customerId);
        }

        public async Task<int> CreateOrderAsync(Order order)
        {
            var carts = await m_OrderItemService.GetOrderItemLinesAsync(order.Id);
            if (carts == null)
                return -1;

            decimal totalAmount = 0;
            foreach (var cart in carts)
            {
                var item = await m_ProductService.GetProductByIdAsync(cart.ProductId);
                if (item != null)
                    totalAmount += item.Price;
            }

            order.TotalAmount = totalAmount;
            order.OrderDate = DateTime.UtcNow;

            await m_OrderRepository.AddAsync(order);
            return order.Id;
        }

        public async Task CancelOrderAsync(int orderId)
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