using Domain.Enums;

namespace Application.DTOs;

public class StockTransferDto
{
    public Guid Id { get; set; }
    public Guid FromStoreId { get; set; }
    public Guid ToStoreId { get; set; }
    public TransferStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}