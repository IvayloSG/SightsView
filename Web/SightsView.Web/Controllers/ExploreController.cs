namespace SightsView.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Explore;

    public class ExploreController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICreationsService creationsService;

        public ExploreController(ICategoriesService categoriesService, ICreationsService creationsService)
        {
            this.categoriesService = categoriesService;
            this.creationsService = creationsService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var categoryCount = 3;

            var topCategories = await this.categoriesService.GetTopCategoriesAsync<CategoryViewModel>(categoryCount);

            var currentCategory = id;

            var creationsCount = GlobalConstants.CreationsPerPage;

            var creations = await this.creationsService.GetNewestCreationsByCategoryAsync<CreationsViewModel>(currentCategory, creationsCount);

            var viewModel = new ExploreIndexViewModel()
            {
                TopCategories = topCategories,
                Creations = creations,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Search(ExploreIndexViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.SearchInput))
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var categoryCount = 3;
            var topCategories = await this.categoriesService.GetTopCategoriesAsync<CategoryViewModel>(categoryCount);

            var formattedInput = input.SearchInput.Replace("#", string.Empty);
            var creationsCount = GlobalConstants.CreationsPerPage;

            var creations = await this.creationsService.GetCreationsByNameOrTagAsync<CreationsViewModel>(formattedInput, creationsCount);

            var viewModel = new ExploreIndexViewModel()
            {
                TopCategories = topCategories,
                Creations = creations,
            };

            return this.View(nameof(this.Index), viewModel);
        }
    }
}
