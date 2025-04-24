using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Data.Entities;


namespace Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> GetAllAsync()
        {
            var items = await _itemRepository.GetAllAsync();
            return _mapper.Map<List<ItemDto>>(items);
        }

        public async Task<ItemDto?> GetByIdAsync(int id)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task CreateAsync(CreateItemDto dto)
        {
            var item = _mapper.Map<Item>(dto);
            await _itemRepository.AddAsync(item);
        }

        public async Task UpdateAsync(int id, UpdateItemDto dto)
        {
            var item = await _itemRepository.GetByIdAsync(id);
            if (item == null) return;

            _mapper.Map(dto, item);
            await _itemRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _itemRepository.DeleteAsync(id);
        }

        public async Task<List<ItemDto>> GetByTypeAsync(string type)
        {
            var items = await _itemRepository.GetByTypeAsync(type);
            return _mapper.Map<List<ItemDto>>(items);
        }
    }
}
