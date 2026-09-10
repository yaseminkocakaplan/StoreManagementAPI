using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Stores.Queries;

// MediatR İstek Tanımı (Dönüş tipi: List<StoreDto>)
public class GetAllStoresQuery : IRequest<List<StoreDto>>
{
}

// MediatR İstek İşleyicisi (Handler)
public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoresQuery, List<StoreDto>>
{
    private readonly IGenericRepository<Store> _storeRepository;

    public GetAllStoresQueryHandler(IGenericRepository<Store> storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<List<StoreDto>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
    {
        var stores = await _storeRepository.GetAllAsync();

        return stores.Select(s => new StoreDto
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            IsActive = s.IsActive
        }).ToList();
    }
}