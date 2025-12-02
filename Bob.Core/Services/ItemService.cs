using Bob.Core.Domain;
using Bob.Core.DTO;
using Bob.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
    public class ItemService
    {
        private readonly IItemRepository m_ItemRepository;

        public ItemService(IItemRepository repository)
        {
            m_ItemRepository = repository;
        }

        public async Task<ItemDto> GetItemByIdAsync(uint itemId)
        {
            Item item = await m_ItemRepository.GetByIdAsync(itemId);
            if (item == null)
            {
                return null;
            }

            return new ItemDto(item.Id, item.Name, item.Size, item.Color, item.Price);
        }

        public async Task<IReadOnlyList<ItemDto>> GetAllItemsAsync()
        {
            var items = await m_ItemRepository.GetAllAsync();
            return items.Select(i => new ItemDto(i.Id, i.Name, i.Size, i.Color, i.Price)).ToList();
        }

        public async Task AddItemAsync(ItemDto dto)
        {
            var entity = new Item(
                dto.ItemId,
                dto.Name,
                dto.Size,
                dto.Color,
                dto.Price
            );
            await m_ItemRepository.AddAsync(entity);
        }

        public async Task UpdateItemAsync(ItemDto dto)
        {
            var entity = new Item(
                dto.ItemId,
                dto.Name,
                dto.Size,
                dto.Color,
                dto.Price
            );

            await m_ItemRepository.UpdateAsync(entity);
        }

        public async Task DeleteItemAsync(uint itemId) => await m_ItemRepository.DeleteAsync(itemId);
    }
}
