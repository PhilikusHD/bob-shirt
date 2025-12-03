using Bob.Core.Domain;
using Bob.Core.Logging;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class CartService
    {
        private readonly CartRepository m_CartRepository;
        private readonly ItemService m_ItemService;

        public CartService(CartRepository cartRepository, ItemService itemService)
        {
            m_CartRepository = cartRepository;
            m_ItemService = itemService;
        }

        public async Task<IReadOnlyList<CartLine>> GetCartLinesAsync(CartId cartId)
        {
            return await m_CartRepository.GetCartLinesAsync(cartId);
        }

        public async Task AddLineAsync(CartLine line)
        {
            var item = await m_ItemService.GetItemByIdAsync(line.ItemId);
            if (item == null)
            {
                Logger.Error("Item does not exist.");
                return;
            }

            await m_CartRepository.AddLineAsync(line);
        }

        public async Task RemoveLineAsync(CartId cartId, ItemId itemId) => await m_CartRepository.RemoveLineAsync(cartId, itemId);

        public async Task AssignToOrderAsync(CartId cartId, OrderId orderId) => await m_CartRepository.AssignToOrderAsync(cartId, orderId);
    }
}
