namespace SightsView.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using Xunit;

    public class FollowsServiceTests : BaseServiceTests
    {
        private IFollowsService Service => this.ServiceProvider.GetRequiredService<IFollowsService>();

        [Fact]
        public async Task FollowUnfollowAsyncFollowResultTest()
        {
            var followedId = "13";
            var followerId = "7";

            await this.Service.FollowUnfollowAsync(followerId, followedId);

            var result = await this.DbContext.Follows.FirstOrDefaultAsync();

            Assert.Equal(followerId, result.FollowerId);
            Assert.Equal(followedId, result.FollowedId);
        }

        [Fact]
        public async Task FollowUnfollowAsyncUnfollowResultTest()
        {
            var followedId = "13";
            var followerId = "7";

            await this.Service.FollowUnfollowAsync(followerId, followedId);
            var resultAfterFollow = await this.DbContext.Follows.FirstOrDefaultAsync();

            await this.Service.FollowUnfollowAsync(followerId, followedId);
            var resultAfterUnfollow = await this.DbContext.Follows.FirstOrDefaultAsync();

            Assert.Equal(followerId, resultAfterFollow.FollowerId);
            Assert.Equal(followedId, resultAfterFollow.FollowedId);
            Assert.Null(resultAfterUnfollow);
        }

        [Fact]
        public async Task FollowUnfollowAsyncReturnResultTest()
        {
            var followedId = "13";
            var followerId = "13";

            await this.Service.FollowUnfollowAsync(followerId, followedId);

            var result = await this.DbContext.Follows.FirstOrDefaultAsync();

            Assert.Null(result);
        }

        [Fact]
        public async Task GetFollowersCountResultTest()
        {
            var follow = new Follow()
            {
                FollowedId = "13",
                FollowerId = "7",
            };

            var followTwo = new Follow()
            {
                FollowedId = "7",
                FollowerId = "10",
            };

            var followThree = new Follow()
            {
                FollowedId = "13",
                FollowerId = "10",
            };

            await this.DbContext.Follows.AddAsync(follow);
            await this.DbContext.Follows.AddAsync(followTwo);
            await this.DbContext.Follows.AddAsync(followThree);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetFollowersCountAsync(follow.FollowedId);

            var expectedCount = 2;

            Assert.Equal(expectedCount, result);
        }
    }
}
