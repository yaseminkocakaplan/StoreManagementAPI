using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.StockTransfers.Commands;

public class TransferItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateStockTransferCommand : IRequest<StockTransferDto>
{
    public Guid FromStoreId { get; set; }
    public Guid ToStoreId { get; set; }
    public List<TransferItemRequest> Items { get; set; } = new();
}

public class CreateStockTransferCommandHandler : IRequestHandler<CreateStockTransferCommand, StockTransferDto>
{
    private readonly IGenericRepository<StockTransfer> _transferRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStockTransferCommandHandler(IGenericRepository<StockTransfer> transferRepository, IUnitOfWork unitOfWork)
    {
        _transferRepository = transferRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StockTransferDto> Handle(CreateStockTransferCommand request, CancellationToken cancellationToken)
    {
        var transfer = new StockTransfer
        {
            FromStoreId = request.FromStoreId,
            ToStoreId = request.ToStoreId,
            Status = TransferStatus.Pending,
        };

        await _transferRepository.AddAsync(transfer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new StockTransferDto
        {
            Id = transfer.Id,
            FromStoreId = transfer.FromStoreId,
            ToStoreId = transfer.ToStoreId,
            Status = transfer.Status,
            CreatedDate = transfer.CreatedDate
        };
    }
}