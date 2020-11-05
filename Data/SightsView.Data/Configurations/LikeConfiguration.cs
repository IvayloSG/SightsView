namespace SightsView.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SightsView.Data.Models;

    class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> like)
        {
            like.HasKey(k => new { k.ApplicationUserId, k.CreationId });
        }
    }
}
