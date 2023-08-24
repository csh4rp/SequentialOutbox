using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SequentialOutbox.Infrastructure.Outbox.Entities;

namespace SequentialOutbox.Infrastructure.Database.EntityConfigurations;

internal sealed class OutboxMessageGenerationEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessageGeneration>
{
    public void Configure(EntityTypeBuilder<OutboxMessageGeneration> builder)
    {
        builder.ToTable(nameof(OutboxMessageGeneration));
        
        builder.HasKey(b => b.Id);
    }
}