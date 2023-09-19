using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewAvalon.Persistence.Relational.Constants;

namespace NewAvalon.Persistence.Relational.Outbox
{
    internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(TableNames.Messages);

            builder.HasKey(message => message.Id);

            builder.Property(message => message.Content).IsRequired().HasColumnType("jsonb");

            builder.Property(message => message.CreatedOnUtc).IsRequired();

            builder.Property(message => message.ModifiedOnUtc).IsRequired(false);

            builder.Property(message => message.Error).IsRequired(false);
        }
    }
}
