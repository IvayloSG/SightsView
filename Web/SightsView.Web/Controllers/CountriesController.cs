namespace SightsView.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Countries;

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

            if (name == null)
            {
                return this.NotFound(
                    string.Format(ExceptionMessages.CategoryNotFound, id));
            }

            var viewModel = new CountriesCreationsViewModel()
            {
                Id = id,
                Name = name,
            };

            return this.View(viewModel);
        }
    }
}
