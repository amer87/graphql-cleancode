using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Com.Domain.Entities;

namespace Com.Persistence.Configurations;
class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder
            .ToTable("Items");

        builder
            .HasKey(i => i.Id);

        builder
            .Property(i => i.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder
            .Property(i => i.Description)
            .HasMaxLength(100);

        builder
            .Property(i => i.SystemReference)
            .HasMaxLength(20);

        builder
            .HasIndex(i => new { i.Type, i.ShopId })
            .HasDatabaseName("IX_Items_Type_Shop");

        builder.HasOne(i => i.Owner)
            .WithMany(c => c.Items)
            .HasConstraintName("FK_User_Items")
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Shop)
            .WithMany(c => c.Items)
            .HasConstraintName("FK_Shop_Items")
            .HasForeignKey(i => i.ShopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Ignore(i => i.Files);
    }
}
