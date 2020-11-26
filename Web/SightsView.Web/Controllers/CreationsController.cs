namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Data;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;
    using SightsView.Web.ViewModels.Creations;

    public class CreationsController : Controller
    {
        private readonly ITagsExtractingService tagsExtractingService;
        private readonly ICreationsService creationsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICountriesService countriesService;
        private readonly ITagsService tagsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreationsController(
            ITagsExtractingService tagsExtractingService,
            ICreationsService creationsService,
            ICategoriesService categoriesService,
            ICountriesService countriesService,
            ITagsService tagsService,
            UserManager<ApplicationUser> userManager)
        {
            this.creationsService = creationsService;
            this.categoriesService = categoriesService;
            this.countriesService = countriesService;
            this.userManager = userManager;
            this.tagsExtractingService = tagsExtractingService;
            this.tagsService = tagsService;
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
            // TODO: Remove
            input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
            input.Countries = await this.countriesService.GetSelectListCountriesAsync();

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
                // TODO: Remove hardcoded string
                var otherCategory = await this.categoriesService.GetCategoryByNameAsync<CategoryViewModel>("Other");
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

            var filePath = await this.creationsService.AddCreationInDbAsync(input.Title, input.Description, isPrivate, countryId, categoryId, user, input.Creation, tags);

            return this.View(input);
        }

        [Authorize]
        public async Task<IActionResult> Load(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = await this.creationsService.GetCreationByIdAsync<CreationsViewModel>(id);

            if (viewModel.CreatorId != userId)
            {
                viewModel.Views++;
                await this.creationsService.IncreseCreationViewsAsync(id);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = await this.creationsService.GetCreationByIdAsync<CreationsEditInputModel>(id);

            viewModel.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
            viewModel.Countries = await this.countriesService.GetSelectListCountriesAsync();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(CreationsEditInputModel input)
        {
            // TODO: remove SelectListItemCreation after final routing
            input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
            input.Countries = await this.countriesService.GetSelectListCountriesAsync();

            if (!this.ModelState.IsValid)
            {
                input = await this.creationsService.GetCreationByIdAsync<CreationsEditInputModel>(input.Id);

                input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
                input.Countries = await this.countriesService.GetSelectListCountriesAsync();

                return this.View(input);
            }

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
            if (input.CategoryName != null)
            {
                countryId = int.Parse(input.CountryName);
            }

            var response = await this.creationsService.EditCreationByIdAsync(input.Id, input.Title, input.Description, isPrivate, categoryId, countryId, userId);

            return this.View(input);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var deleteResponse = await this.creationsService.DeleteCreationAsync(id, userId);

            return this.RedirectToAction("Index", "Home");
        }

    }
}
