namespace SightsView.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using Xunit;

    public class LikesServiceTests : BaseServiceTests
    {
        private ILikesService Service => this.ServiceProvider.GetRequiredService<ILikesService>();

        [Fact]
        public async Task GetLikesCountResultTest()
        {
            var like = new Like()
            {
                ApplicationUserId = "13",
                CreationId = "7",
            };

            var likeTwo = new Like()
            {
                ApplicationUserId = "7",
                CreationId = "10",
            };

            var likeThree = new Like()
            {
                ApplicationUserId = "13",
                CreationId = "10",
            };

            await this.DbContext.Likes.AddAsync(like);
            await this.DbContext.Likes.AddAsync(likeTwo);
            await this.DbContext.Likes.AddAsync(likeThree);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetLikesCountAsync(likeTwo.CreationId);

            var expectedCount = 2;

            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task LikeUnlikeAsyncLikeResultTest()
        {
            var userId = "13";
            var creationId = "7";

            await this.Service.LikeUnlikeAsync(userId, creationId);

            var result = await this.DbContext.Likes.FirstOrDefaultAsync();

            Assert.Equal(userId, result.ApplicationUserId);
            Assert.Equal(creationId, result.CreationId);
        }

        [Fact]
        public async Task FollowUnfollowAsyncUnfollowResultTest()
        {
            var userId = "13";
            var creationId = "7";

            await this.Service.LikeUnlikeAsync(userId, creationId);
            var resultAfterFollow = await this.DbContext.Likes.FirstOrDefaultAsync();

            await this.Service.LikeUnlikeAsync(userId, creationId);
            var resultAfterUnfollow = await this.DbContext.Likes.FirstOrDefaultAsync();

            Assert.Equal(userId, resultAfterFollow.ApplicationUserId);
            Assert.Equal(creationId, resultAfterFollow.CreationId);
            Assert.Null(resultAfterUnfollow);
        }
    }
}
