using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.StockTransfers.Commands;

public class UpdateTransferStatusCommand : IRequest<StockTransferDto?>
{
    public Guid TransferId { get; set; }
    public TransferStatus NewStatus { get; set; }
}

public class UpdateTransferStatusCommandHandler : IRequestHandler<UpdateTransferStatusCommand, StockTransferDto?>
{
    private readonly IGenericRepository<StockTransfer> _transferRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTransferStatusCommandHandler(IGenericRepository<StockTransfer> transferRepository, IUnitOfWork unitOfWork)
    {
        _transferRepository = transferRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<StockTransferDto?> Handle(UpdateTransferStatusCommand request, CancellationToken cancellationToken)
    {
        var transfer = await _transferRepository.GetByIdAsync(request.TransferId);

        if (transfer == null)
            return null;

        transfer.Status = request.NewStatus;
        _transferRepository.Update(transfer);
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