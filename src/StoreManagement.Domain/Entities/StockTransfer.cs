using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class StockTransfer : BaseEntity
{
    public Guid FromStoreId { get; set; }
    public Store FromStore { get; set; } = null!;

    public Guid ToStoreId { get; set; }
    public Store ToStore { get; set; } = null!;

    public TransferStatus Status { get; set; } = TransferStatus.Pending;

    public ICollection<StockTransferItem> Items { get; set; } = new List<StockTransferItem>();
}