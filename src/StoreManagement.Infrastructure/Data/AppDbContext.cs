using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<StockTransfer> StockTransfers => Set<StockTransfer>();
    public DbSet<StockTransferItem> StockTransferItems => Set<StockTransferItem>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Store - StockTransfer İlişkileri (Çoklu Foreign Key çakışmasını önlemek için)
        modelBuilder.Entity<StockTransfer>()
            .HasOne(st => st.FromStore)
            .WithMany()
            .HasForeignKey(st => st.FromStoreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StockTransfer>()
            .HasOne(st => st.ToStore)
            .WithMany()
            .HasForeignKey(st => st.ToStoreId)
            .OnDelete(DeleteBehavior.Restrict);

        // Decimal alanlar için SQL hassasiyet ayarı
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}