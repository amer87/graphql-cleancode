using Com.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Persistence.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(t => t.UserName).IsUnique();
        builder.HasIndex(t => t.ShopId).HasDatabaseName("IX_SHOP_USER");
        builder.HasMany(a => a.Items).WithOne(f => f.Owner).HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Restrict);
    }
}