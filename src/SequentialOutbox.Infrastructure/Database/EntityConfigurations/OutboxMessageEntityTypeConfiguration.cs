using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SequentialOutbox.Infrastructure.Outbox.Entities;

namespace SequentialOutbox.Infrastructure.Database.EntityConfigurations;

internal sealed class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(nameof(OutboxMessage));
        
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Payload)
            .IsRequired();

        builder.Property(b => b.PayloadType)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(b => b.Stream)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(b => b.Topic)
            .IsRequired()
            .HasMaxLength(128);
    }
}