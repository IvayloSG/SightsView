namespace SightsView.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SightsView.Data.Models;

    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> message)
        {
            message.HasOne(c => c.Sender)
                     .WithMany(m => m.SentMessages)
                     .HasForeignKey(c => c.SenderId);

            message.HasOne(c => c.Receiver)
               .WithMany(m => m.ReceivedMessages)
               .HasForeignKey(c => c.ReceiverId);
        }
    }
}
