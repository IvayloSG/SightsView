namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;
    using SightsView.Web.ViewModels.Comments;
    using SightsView.Web.ViewModels.Creations;

    public class CreationsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICommentsService comentsService;
        private readonly ICountriesService countriesService;
        private readonly ICreationsService creationsService;
        private readonly ITagsExtractingService tagsExtractingService;
        private readonly ITagsService tagsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreationsController(
            ICategoriesService categoriesService,
            ICommentsService commentsService,
            ICountriesService countriesService,
            ICreationsService creationsService,
            ITagsExtractingService tagsExtractingService,
            ITagsService tagsService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.comentsService = commentsService;
            this.countriesService = countriesService;
            this.creationsService = creationsService;
            this.tagsExtractingService = tagsExtractingService;
            this.tagsService = tagsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var isSuccess = await this.creationsService.DeleteCreationAsync(id, userId);
                if (!isSuccess)
                {
                    return this.RedirectToAction("LoadRedirect", "Creations", new { id = id });
                }

                return this.RedirectToAction("Index", "Sights");
            }
            catch (NullReferenceException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<CreationsDetailsViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CreationNotFound, id));
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Equipment(string id)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<CreationsEquipmentViewModel>(id);
            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CreationNotFound, id));
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var viewModel = await this.creationsService.GetCreationModelByIdAsync<CreationsEditInputModel>(id);
            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CreationNotFound, id));
            }

            viewModel.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
            viewModel.Countries = await this.countriesService.GetSelectListCountriesAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreationsEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input = await this.creationsService.GetCreationModelByIdAsync<CreationsEditInputModel>(input.Id);

                input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
                input.Countries = await this.countriesService.GetSelectListCountriesAsync();

                return this.View(input);
            }

            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                bool isPrivate = false;
                if (input.Privacy.ToLower() == "private")
                {
                    isPrivate = true;
                }

                int? categoryId = null;
                if (input.CategoryName != null)
                {
                    categoryId = int.Parse(input.CategoryName);
                }

                int? countryId = null;
                if (input.CountryName != null)
                {
                    countryId = int.Parse(input.CountryName);
                }

                var isSuccess = await this.creationsService.EditCreationByIdAsync(input.Id, input.Title, input.Description, isPrivate, categoryId, countryId, userId);

                return this.RedirectToAction("LoadRedirect", "Creations", new { id = input.Id });
            }
            catch (NullReferenceException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [Authorize]
        public async Task<IActionResult> Load(string id)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var creationViewModel = await this.creationsService.GetCreationModelByIdAsync<CreationsViewModel>(id);
                if (creationViewModel == null)
                {
                    return this.NotFound(
                        string.Format(ExceptionMessages.CreationNotFound, id));
                }

                if (creationViewModel.CreatorId != userId)
                {
                    creationViewModel.Views++;
                    await this.creationsService.IncreseCreationViewsAsync(id);
                }

                var comments = await this.comentsService.GetAllCommentsForCreationAsync<CommentsAllViewModel>(id);

                var viewModel = new CreationsLoadViewModel()
                {
                    Creation = creationViewModel,
                    Comments = comments,
                };

                return this.View(viewModel);
            }
            catch (NullReferenceException e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [Authorize]
        public async Task<IActionResult> LoadRedirect(string id)
        {
            var creationViewModel = await this.creationsService.GetCreationModelByIdAsync<CreationsViewModel>(id);
            if (creationViewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CreationNotFound, id));
            }

            var comments = await this.comentsService.GetAllCommentsForCreationAsync<CommentsAllViewModel>(id);

            var viewModel = new CreationsLoadViewModel()
            {
                Creation = creationViewModel,
                Comments = comments,
            };

            return this.View("Load", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Upload()
        {
            var viewModel = new CreationsUploadInputModel()
            {
                Categories = await this.categoriesService.GetSelectListCategoriesAsync(),
                Countries = await this.countriesService.GetSelectListCountriesAsync(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload(CreationsUploadInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
                input.Countries = await this.countriesService.GetSelectListCountriesAsync();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            bool isPrivate = false;
            if (input.Privacy.ToLower() == "private")
            {
                isPrivate = true;
            }

            int? countryId = int.Parse(input.Country);
            if (countryId == 0)
            {
                countryId = null;
            }

            int categoryId = int.Parse(input.Category);
            if (categoryId == 0)
            {
                var otherCategory = await this.categoriesService.GetCategoryByNameAsync<CategoriesViewModel>(GlobalConstants.CategoryOtherName);
                categoryId = otherCategory.Id;
            }

            var tagsInput = input.Tags;

            if (tagsInput == null)
            {
                tagsInput = string.Empty;
            }

            var tagNames = this.tagsExtractingService.GetTagsFromTagsArea(tagsInput);

            await this.tagsService.CreateTagsAsync(tagNames);
            var tags = await this.tagsService.GetTagsByNameAsync(tagNames);

            var creationId = await this.creationsService.AddCreationAsync(input.Title, input.Description, isPrivate, countryId, categoryId, user, input.Creation, tags);

            return this.RedirectToAction("LoadRedirect", "Creations", new { id = creationId });
        }
    }
}
