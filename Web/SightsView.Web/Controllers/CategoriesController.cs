namespace SightsView.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await this.categoriesService.GetAllCategoriesAsync<CategoriesViewModel>();

                var viewModel = new CategoriesAllViewModel()
                {
                    AlphabeticalCategoryList = categories,
                };

                return this.View(viewModel);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Add(CategoryAddInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                await this.categoriesService.CreateCategoryAsync(input.Name, input.Description);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.categoriesService.DeleteCategoryByIdAsync(id);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.categoriesService.GetCategoryByIdAsync<CategoriesViewModel>(id);

            if (viewModel == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CategoryNotFound, id));
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(CategoryEditInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var viewModel = await this.categoriesService.GetCategoryByIdAsync<CategoriesViewModel>(input.Id);
                return this.View(viewModel);
            }

            try
            {
                await this.categoriesService.UpdateCategoryByIdAsync(input.Id, input.Name, input.Description);

                return this.RedirectToAction(nameof(this.Index));
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
