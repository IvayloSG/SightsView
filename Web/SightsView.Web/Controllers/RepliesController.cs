namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Replies;

    public class RepliesController : Controller
    {
        private readonly IRepliesService repliesService;
        private readonly ICommentsService commentsService;

        public RepliesController(IRepliesService repliesService, ICommentsService commentsService)
        {
            this.repliesService = repliesService;
            this.commentsService = commentsService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add(int id)
        {
            var viewModel = new RepliesAddInputModel()
            {
                CommentId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(RepliesAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = new RepliesAddInputModel()
                {
                    CommentId = input.CommentId,
                };

                return this.View(viewModel);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.repliesService.AddReplyToCommentAsync(input.Content, input.CommentId, userId);

            var creationId = await this.commentsService.GetCreationIdByCommentAsync(input.CommentId);

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = creationId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var result = await this.repliesService.DeleteReplyAsync(id, userId);
                if (result == false)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                return this.RedirectToAction();
            }
            catch (Exception nre)
            {
                return this.BadRequest(nre.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.repliesService.GetReplyByIdAsync<RepliesEditViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.ReplyNotFound, id));
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(RepliesEditViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = await this.commentsService.GetCommentsByIdAsync<RepliesEditViewModel>(input.Id);

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

                var commentId = await this.repliesService.EditReplyAsync(input.Id, input.Content, userId);

                if (commentId == 0)
                {
                    return this.RedirectToAction("Index", "Home");
                }

                var creationId = await this.commentsService.GetCreationIdByCommentAsync(commentId);

                return this.RedirectToAction("LoadRedirect", "Creations", new { id = creationId });
            }
            catch (Exception nre)
            {
                return this.BadRequest(nre.Message);
            }
        }
    }
}
