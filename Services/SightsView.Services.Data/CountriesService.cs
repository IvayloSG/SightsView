namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using SightsView.Common;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Countries;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countriesRepository;
        private readonly IRandomiseService randomiseService;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository, IRandomiseService randomiseService)
        {
            this.countriesRepository = countriesRepository;
            this.randomiseService = randomiseService;
        }

        public async Task<IEnumerable<CountriesViewModel>> GetCountriesWithMostCreationAsync(int countriesCount)
        {
            try
            {
                var topCountries = await this.countriesRepository.AllAsNoTracking()
                 .Where(x => x.Creations.Count > 0 && x.Creations.Select(c => c.IsPrivate == false).FirstOrDefault())
                 .OrderByDescending(x => x.Creations.Count)
                 .Select(x => new CountriesViewModel()
                 {
                     Id = x.Id,
                     Name = x.Name,
                     DataURL = this.randomiseService.GetRandomElement<Creation>(x.Creations.ToList()).CreationDataUrl,
                 })
                 .Take(countriesCount)
                 .ToListAsync();

                return topCountries;
            }
            catch (System.Exception e)
            {

                throw new System.Exception(e.Message);
            }

        }

        public async Task<IList<SelectListItem>> GetSelectListCountriesAsync()
        {
            var selectListItem = await this.countriesRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            })
            .ToListAsync();

            var noCountryOption = new SelectListItem(GlobalConstants.NoCountryOption, 0.ToString());

            selectListItem.Insert(0, noCountryOption);

            return selectListItem;
        }
    }
}
