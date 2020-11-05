namespace SightsView.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SightsView.Data.Models;

    public class CreationConfiguration : IEntityTypeConfiguration<Creation>
    {
        public void Configure(EntityTypeBuilder<Creation> creation)
        {
            creation.HasOne(c => c.Creator)
                 .WithMany(p => p.Creations)
                 .HasForeignKey(c => c.CreatorId);
        }
    }
}
