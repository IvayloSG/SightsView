namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Likes;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<PostLikeResponseModel> Like(LikesInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.likesService.LikeUnlikeAsync(userId, input.CreationId);

            var likes = await this.likesService.GetLikesCountAsync(input.CreationId);

            return new PostLikeResponseModel { Likes = likes };
        }
    }
}
