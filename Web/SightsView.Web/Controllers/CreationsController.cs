namespace SightsView.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Services.Data.Contracts;

    public class CreationsController : Controller
    {
        private ICategoriesService categoriesService;
        private ICountriesService countriesService;

        public CreationsController(ICategoriesService categoriesService, 
            ICountriesService countriesService)
        {
            this.categoriesService = categoriesService;
            this.countriesService = countriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Upload()
        {
            this.ViewData["Categories"] = await this.categoriesService.GetSelectListCategoriesAsync();

            this.ViewData["Countries"] = await this.countriesService.GetSelectListCountriesAsync();

            return this.View();
        }
    }
}
