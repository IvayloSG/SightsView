namespace SightsView.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Countries;
    using SightsView.Web.ViewModels.Creations;

    public class CountriesController : Controller
    {
        private readonly ICountriesService countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            var name = await this.countriesService.GetCountryNameByIdAsync(id);

            var viewModel = new CountriesCreationsViewModel()
            {
                Id = id,
                Name = name,
            };

            return this.View(viewModel);
        }
    }
}
