namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Follows;

    [ApiController]
    [Route("api/[controller]")]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowsService followsService;

        public FollowsController(IFollowsService followsService)
        {
            this.followsService = followsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<PostFollowResponseModel> Follow(FollowInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == input.FollowedId)
            {
                var nonModifiedFollowers = await this.followsService.GetFollowersCountAsync(input.FollowedId);
                return new PostFollowResponseModel { Followers = nonModifiedFollowers };
            }

            await this.followsService.FollowUnfollowAsync(userId, input.FollowedId);

            var followers = await this.followsService.GetFollowersCountAsync(input.FollowedId);

            return new PostFollowResponseModel { Followers = followers };
        }
    }
}
