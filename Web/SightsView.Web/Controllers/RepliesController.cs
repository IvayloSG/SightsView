namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Replies;

    public class RepliesController : Controller
    {
        private readonly IRepliesService repliesService;

        public RepliesController(IRepliesService repliesService)
        {
            this.repliesService = repliesService;
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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.repliesService.AddReplyToCommentAsync(input.Content, input.CommentId, userId);

            return this.RedirectToAction();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await this.repliesService.DeleteReplyAsync(id, userId);

            return this.RedirectToAction();
        }
    }
}
