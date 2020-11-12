namespace SightsView.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await this.categoriesService.GetAllCategoriesAsync<CategoryViewModel>();

            var viewModel = new CategoriesAllViewModel()
            {
                AlphabeticalCategoryList = categories,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.CreateCategoryAsync(input.Name, input.Description);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.categoriesService.GetCategoryByIdAsync<CategoryViewModel>(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var isUpdateSuccessful = await this.categoriesService.UpdateCategoryByIdAsync(input.Id, input.Name, input.Description);

            // TODO: To implement error when update is unsuccessful;
            if (!isUpdateSuccessful)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var isUpdateSuccessful = await this.categoriesService.DeleteCategoryByIdAsync(id);

            // TODO: To implement error when update is unsuccessful;
            if (!isUpdateSuccessful)
            {
                return this.View();
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
