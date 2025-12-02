using Bob.Core.Domain;
using Bob.Core.DTO;
using Bob.Core.Logging;
using Bob.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class CartService
    {
        private readonly ICartRepository m_CartRepository;
        private readonly ItemService m_ItemService;

        public CartService(ICartRepository cartRepository, ItemService itemService)
        {
            m_CartRepository = cartRepository;
            m_ItemService = itemService;
        }

        public async Task<IReadOnlyList<CartDto>> GetCartLinesAsync(uint cartId)
        {
            var cartLines = await m_CartRepository.GetCartLinesAsync(cartId);
            if (cartLines == null)
            {
                return null;
            }

            return cartLines.Select(c => new CartDto(c.CartId, c.ItemId, (c.OrderId == null) ? 0xFFFFFF : c.OrderId.Value)).ToList();
        }

        public async Task AddLineAsync(CartDto line)
        {
            var item = await m_ItemService.GetItemByIdAsync(line.ItemId);
            if (item == null)
            {
                Logger.Error("Item does not exist.");
                return;
            }

            await m_CartRepository.AddLineAsync(new CartLine(line.CartId, line.ItemId, line.OrderId));
        }

        public async Task RemoveLineAsync(uint cartId, uint itemId) => await m_CartRepository.RemoveLineAsync(cartId, itemId);

        public async Task AssignToOrderAsync(uint cartId)
        {
            // TODO
            // I really don't feel like it rn omfg
        }
    }
}
