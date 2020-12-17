namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Countries;
    using SightsView.Web.ViewModels.Creations;

    public class CountriesController : Controller
    {
        private readonly ICreationsService creationsService;

        public CountriesController(ICreationsService creationsService)
        {
            this.creationsService = creationsService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var creationsCount = 30;
            var creations = await this.creationsService.GetCreationByCountryAsync<CreationsViewModel>(id, creationsCount);

            var countryName = creations.FirstOrDefault().CountryName;

            var viewModel = new CountriesCreationsViewModel()
            {
                Name = countryName,
                Creations = creations,
            };

            return this.View(viewModel);
        }
    }
}
