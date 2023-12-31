using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Database.EntityConfigurations;

internal sealed class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product));
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(b => b.Price);
    }
}