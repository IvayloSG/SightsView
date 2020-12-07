namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
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

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = input.CreationId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var creationId = await this.commentsService.DeleteCommentAsync(id, userId);

            if (creationId == null)
            {
                return this.RedirectToAction();
            }

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = creationId });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.commentsService.GetCommentsByIdAsync<CommentsEditViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CommentsEditViewModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this.commentsService.EditCommentAsync(input.Id, input.Content, userId);

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = input.CreationId });
        }
    }
}
