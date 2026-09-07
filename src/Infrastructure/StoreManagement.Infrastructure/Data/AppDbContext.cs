using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();
    public DbSet<StockTransfer> StockTransfers => Set<StockTransfer>();
    public DbSet<StockTransferItem> StockTransferItems => Set<StockTransferItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Price).HasPrecision(18, 2);
            entity.HasOne(p => p.Store)
                  .WithMany(s => s.Products)
                  .HasForeignKey(p => p.StoreId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // StockTransfer (Çift Mağaza İlişkisi)
        modelBuilder.Entity<StockTransfer>(entity =>
        {
            entity.HasOne(st => st.FromStore)
                  .WithMany()
                  .HasForeignKey(st => st.FromStoreId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(st => st.ToStore)
                  .WithMany()
                  .HasForeignKey(st => st.ToStoreId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // StockTransferItem Konfigürasyonu
        modelBuilder.Entity<StockTransferItem>(entity =>
        {
            entity.HasOne(sti => sti.StockTransfer)
                  .WithMany(st => st.Items)
                  .HasForeignKey(sti => sti.StockTransferId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sti => sti.Product)
                  .WithMany()
                  .HasForeignKey(sti => sti.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}