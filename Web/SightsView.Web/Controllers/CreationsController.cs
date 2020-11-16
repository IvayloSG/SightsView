namespace SightsView.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Creations;

    public class CreationsController : Controller
    {
        private readonly ICreationsService creationsService;
        private readonly ICategoriesService categoriesService;
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CreationsController(
            ICreationsService creationsService,
            ICategoriesService categoriesService,
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.creationsService = creationsService;
            this.categoriesService = categoriesService;
            this.countriesService = countriesService;
            this.userManager = userManager;
        }

        [HttpGet]
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
        public async Task<IActionResult> Upload(CreationsUploadInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Categories = await this.categoriesService.GetSelectListCategoriesAsync();
                input.Countries = await this.countriesService.GetSelectListCountriesAsync();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var image = input.Creation.OpenReadStream();

            return this.View();
        }
    }
}
