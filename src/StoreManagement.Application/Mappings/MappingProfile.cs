using AutoMapper;
using Domain.Entities;
using StoreManagement.Application.DTOs;

namespace StoreManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // 500 hatasını çözen ana satır:
        CreateMap<Product, ProductDto>().ReverseMap();
        
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}