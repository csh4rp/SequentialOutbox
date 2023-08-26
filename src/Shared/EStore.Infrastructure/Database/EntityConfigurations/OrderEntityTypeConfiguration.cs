using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EStore.Domain.Entities;
using EStore.Domain.Enums;

namespace EStore.Infrastructure.Database.EntityConfigurations;

internal sealed class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Email)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(b => b.FirstAddressLine)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(b => b.SecondAddressLine)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(b => b.Status)
            .HasMaxLength(16)
            .IsRequired()
            .HasConversion(b => b.ToString(), b => Enum.Parse<OrderStatus>(b));

        builder.OwnsMany(b => b.Items, b =>
        {
            b.ToTable(nameof(OrderItem));
            
            b.HasKey(o => o.Id);

            b.HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId);
            
            b.WithOwner().HasForeignKey(o => o.OrderId);
        });

    }
}