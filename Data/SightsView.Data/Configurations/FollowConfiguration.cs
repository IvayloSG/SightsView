namespace SightsView.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SightsView.Data.Models;

    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> follow)
        {
            follow.HasOne(f => f.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.FollowedId);

            follow.HasOne(u => u.Follower)
                .WithMany(f => f.Follows)
                .HasForeignKey(u => u.FollowerId);
        }
    }
}
