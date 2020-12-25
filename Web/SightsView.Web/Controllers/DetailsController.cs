namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Details;

    public class DetailsController : Controller
    {
        private readonly IDetailsService detailsService;
        private readonly ICreationsService creationsService;

        public DetailsController(IDetailsService detailsService, ICreationsService creationsService)
        {
            this.detailsService = detailsService;
            this.creationsService = creationsService;
        }

        public IActionResult Add(string id)
        {
            // TODO: Make a redirect to edit action in case equipment exists
            var viewModel = new DetailsAddInputModel()
            {
                CreationId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(DetailsAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                var creatorId = await this.creationsService.GetCreatorIdByCreationIdAsync(input.CreationId);
                var currentUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (creatorId != currentUserID)
                {
                    return this.BadRequest(ExceptionMessages.InvalidUser);
                }

                var detailsId = await this.detailsService.AddDetailsAsync(input.Apereture, input.ShutterSpeed, input.Iso, input.Notes);

                await this.creationsService.AddDetailsToCreationAsync(input.CreationId, detailsId);

                return this.RedirectToAction("Details", "Creations", new { id = input.CreationId });
            }
            catch (NullReferenceException nre)
            {
                return this.NotFound(nre.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<DetailsEditInputModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DetailsEditInputModel input)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<DetailsEditInputModel>(input.Id);
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != input.CreatorId)
            {
                return this.BadRequest();
            }

            if (input.DetailsId == null)
            {
                return this.RedirectToAction(nameof(this.Add), new { id = input.Id });
            }

            var isUpdateSuccessful = await this.detailsService.UpdateDetailsAsync(input.DetailsId, input.DetailsApereture, input.DetailsShutterSpeed, input.DetailsIso, input.DetailsTipAndTricks);

            return this.RedirectToAction("Details", "Creations", new { id = input.Id });
        }
    }
}
