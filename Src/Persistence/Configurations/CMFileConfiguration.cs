using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Com.Domain.Entities;

namespace Com.Persistence.Configurations;

class CMFileConfiguration : IEntityTypeConfiguration<CMFile>
{
    public void Configure(EntityTypeBuilder<CMFile> builder)
    {
        builder.ToTable("CMFiles");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.FileExtention).HasMaxLength(10);
        builder.Property(f => f.Path).HasColumnType("text");
        builder.Property(f => f.OwnerId).IsRequired();
        builder.Property(f => f.Owner).IsRequired();
        builder.HasIndex(f => new { f.Owner, f.OwnerId }).HasDatabaseName("IX_Owner_File");
    }
}
