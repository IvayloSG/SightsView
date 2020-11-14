namespace SightsView.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;

    public class CountriesService : ICountriesService
    {
        private IDeletableEntityRepository<Country> countriesRepository;

        public CountriesService(IDeletableEntityRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
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

            var missingCountryOption = new SelectListItem("No country", 0.ToString());

            selectListItem.Insert(0, missingCountryOption);

            return selectListItem;
        }
    }
}
