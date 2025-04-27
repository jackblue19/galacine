using Application.DTOs;
using AutoMapper;
using Data.Entities;


namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<CreateItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();
        }
    }
}
