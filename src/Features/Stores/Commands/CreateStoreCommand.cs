using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Stores.Commands;

// MediatR İstek Tanımı (Dönüş tipi: StoreDto)
public class CreateStoreCommand : IRequest<StoreDto>
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}

// MediatR İstek İşleyicisi (Handler)
public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, StoreDto>
{
    private readonly IGenericRepository<Store> _storeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStoreCommandHandler(IGenericRepository<Store> storeRepository, IUnitOfWork unitOfWork)
    {
        _storeRepository = storeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StoreDto> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var store = new Store
        {
            Name = request.Name,
            Address = request.Address
        };

        await _storeRepository.AddAsync(store);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new StoreDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address,
            IsActive = store.IsActive
        };
    }
}