using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Com.Domain.Entities;

namespace Com.Persistence.Configurations;
class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title).HasMaxLength(50);
        builder.Property(a => a.AddressLine1).IsRequired().HasMaxLength(100);
        builder.Property(a => a.AddressLine2).HasMaxLength(100);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(50);
        builder.Property(a => a.State).IsRequired().HasMaxLength(50);
        builder.Property(a => a.City).IsRequired().HasMaxLength(50);
        builder.HasIndex(x => new { x.BelongsTo, x.Type }).HasDatabaseName("IX_Owner_Address").IsUnique();
    }
}
