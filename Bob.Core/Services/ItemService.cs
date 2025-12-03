using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
#nullable enable
    public class ItemService
    {
        private readonly ItemRepository m_ItemRepository;

        public ItemService(ItemRepository repository)
        {
            m_ItemRepository = repository;
        }

        public async Task<Item?> GetItemByIdAsync(ItemId itemId)
        {
            return await m_ItemRepository.GetByIdAsync(itemId);
        }

        public async Task<IReadOnlyList<Item>> GetAllItemsAsync()
        {
            return await m_ItemRepository.GetAllAsync();
        }

        public async Task AddItemAsync(Item item)
        {
            await m_ItemRepository.AddAsync(item);
        }

        public async Task UpdateItemAsync(Item item)
        {
            await m_ItemRepository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(ItemId itemId) => await m_ItemRepository.DeleteAsync(itemId);
    }
}
