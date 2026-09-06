using Application.DTOs;
using Application.Features.Products.Commands;
using Application.Features.Stores.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Store Mappings
        CreateMap<Store, StoreDto>().ReverseMap();
        CreateMap<CreateStoreCommand, Store>();

        // Product Mappings
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductCommand, Product>();

        // User Mappings
        CreateMap<User, UserDto>().ReverseMap();

        // StockTransfer Mappings
        CreateMap<StockTransfer, StockTransferDto>().ReverseMap();
    }
}