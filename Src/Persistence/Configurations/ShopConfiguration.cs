using Com.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Persistence.Configurations;
class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder
            .ToTable("Shops");

        builder
            .HasKey(a => a.Id);
        
        builder
            .Property(a => a.Code)
            .HasMaxLength(10)
            .IsRequired();
        
        builder
            .Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(a => a.Email)
            .HasMaxLength(100);

        builder
            .Property(a => a.Description)
            .HasMaxLength(200);

        builder
            .HasMany(a => a.Users)
            .WithOne(f => f.Shop)
            .HasForeignKey(f => f.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Items)
            .WithOne(f => f.Shop)
            .HasForeignKey(f => f.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(a => a.Files);
    }
}
