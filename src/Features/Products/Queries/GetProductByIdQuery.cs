using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Products.Queries;

// MediatR İstek Tanımı (Dönüş tipi: ProductDto?)
public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

// MediatR İstek İşleyicisi (Handler)
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IGenericRepository<Product> _productRepository;

    public GetProductByIdQueryHandler(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
            return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            SKU = product.SKU,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            StoreId = product.StoreId
        };
    }
}