namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
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
        [Authorize]
        public IActionResult Add(string id)
        {
            var viewModel = new CommentsAddInputModel()
            {
                CreationId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CommentsAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new CommentsAddInputModel()
                {
                    CreationId = input.CreationId,
                };

                return this.View(viewModel);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.commentsService.AddCommentAsync(input.Content, input.CreationId, userId);

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = input.CreationId });
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var creationId = await this.commentsService.DeleteCommentAsync(id, userId);

                if (creationId == ExceptionMessages.InvalidUser)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                return this.RedirectToAction("LoadRedirect", "Creations", new { id = creationId });
            }
            catch (NullReferenceException nre)
            {
                return this.BadRequest(nre.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.commentsService.GetCommentsByIdAsync<CommentsEditViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CommentNotFound, id));
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CommentsEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = await this.commentsService.GetCommentsByIdAsync<CommentsEditViewModel>(input.Id);

                if (viewModel == null)
                {
                    return this.NotFound(
                        string.Format(ExceptionMessages.CommentNotFound, input.Id));
                }

                return this.View(viewModel);
            }

            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await this.commentsService.EditCommentAsync(input.Id, input.Content, userId);

                if (result == false)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                return this.RedirectToAction("LoadRedirect", "Creations", new { id = input.CreationId });
            }
            catch (NullReferenceException e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
