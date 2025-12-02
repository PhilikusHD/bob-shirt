using Bob.Core.DTO;
using Bob.Core.Repositories;
using Bob.Core.Domain;
using Bob.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class OrderService
    {
        private readonly IOrderRepository m_OrderRepository;
        private readonly ICartRepository m_CartRepository;
        private readonly IItemRepository m_ItemRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IItemRepository itemRepository)
        {
            m_OrderRepository = orderRepository;
            m_CartRepository = cartRepository;
            m_ItemRepository = itemRepository;
        }

        public async Task<OrderDto> GetOrderByIdAsync(uint orderId)
        {
            Order order = await m_OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;

            return new OrderDto(
                order.Id,
                order.CustomerId,
                order.CartId,
                order.OrderDate,
                order.TotalAmount
            );
        }

        public async Task<IReadOnlyList<OrderDto>> GetOrderForCustomerAsync(uint customerId)
        {
            IReadOnlyList<Order> orders = await m_OrderRepository.GetByCustomerAsync(customerId);
            return orders.Select(o => new OrderDto(
                o.Id,
                o.CustomerId,
                o.CartId,
                o.OrderDate,
                o.TotalAmount
            )).ToList();
        }

        public async Task<uint> CreateOrderAsync(OrderDto orderDto)
        {
            IReadOnlyList<CartLine> cart = await m_CartRepository.GetCartLinesAsync(orderDto.CartId);
            if (cart == null)
                return 0xFFFFFFFF;

            decimal totalAmount = 0;
            // Get Items based on the id
            foreach (CartLine cartLine in cart)
            {
                // Mayhaps we should prefetch all items and then only use the ones needed.
                // It could reduce DB operations.
                var item = await m_ItemRepository.GetByIdAsync(cartLine.ItemId);
                totalAmount += item.Price;
            }

            var orderEntity = new Order
            (
                orderDto.OrderId != 0 ? orderDto.OrderId : 0,
                orderDto.CustomerId,
                orderDto.CartId,
                DateTime.UtcNow,
                totalAmount
            );

            await m_OrderRepository.AddAsync(orderEntity);

            return orderEntity.Id;
        }

        public async Task CancelOrderAsync(uint orderId)
        {
            Order order = await m_OrderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                Logger.Error("Order not found");
                return;
            }

            await m_OrderRepository.DeleteAsync(orderId);
        }
    }
}