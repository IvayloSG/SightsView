namespace SightsView.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SightsView.Data.Models;

    public class TagCreationConfiguration : IEntityTypeConfiguration<TagCreation>
    {
        public void Configure(EntityTypeBuilder<TagCreation> tagCreation)
        {
            tagCreation.HasKey(k => new { k.CreationId, k.TagId });
        }
    }
}
