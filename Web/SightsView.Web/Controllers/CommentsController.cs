namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Comments;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [HttpGet]
        public IActionResult Add(string id)
        {
            var viewModel = new CommentsAddInputModel()
            {
                CreationId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CommentsAddInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.commentsService.AddCommentAsync(input.Content, input.CreationId, userId);

            return this.RedirectToAction();
        }
    }
}
