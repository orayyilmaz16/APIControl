// APIControl.Infrastructure/Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using APIControl.Domain.Entities;

namespace APIControl.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Product config
        b.Entity<Product>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(200);
            e.Property(x => x.Price).HasColumnType("decimal(18,2)");
            e.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        });

        // User config
        b.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).IsRequired().HasMaxLength(150);
            e.Property(x => x.PasswordHash).IsRequired();
            e.Property(x => x.Role).HasMaxLength(50);
            e.Property(x => x.ProductId).IsRequired();
            e.Property(x => x.RefreshToken).HasMaxLength(500);
        });
    }
}
