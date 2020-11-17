namespace SightsView.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels;
    using SightsView.Web.ViewModels.Categories;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly ICreationsService creationsService;

        public HomeController(ICountriesService countriesService, ICreationsService creationsService)
        {
            this.countriesService = countriesService;
            this.creationsService = creationsService;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: Take categories count from config
            int countOfCategories = 4;
            int countOfCreations = 10;
            var topCountries = await this.countriesService.GetCountriesWithMostCreationAsync(countOfCategories);
            var creations = await this.creationsService.GetNumberRandomCreationsAsync(countOfCreations);

            var viewModel = new HomeIndexViewModel()
            {
                CountriesWithMostImages = topCountries,
                Creations = creations,
            };

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
