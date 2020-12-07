namespace SightsView.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var deleteResponse = await this.creationsService.DeleteCreationAsync(id, userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            var viewModel = await this.creationsService.GetDetailsAsync<CreationsDetailsViewModel>(id);

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
        public async Task<IActionResult> Load(string id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var creationViewModel = await this.creationsService.GetCreationByIdAsync<CreationsViewModel>(id);

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

        [Authorize]
        public async Task<IActionResult> LoadRedirect(string id)
        {
            var creationViewModel = await this.creationsService.GetCreationByIdAsync<CreationsViewModel>(id);

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
    }
}
