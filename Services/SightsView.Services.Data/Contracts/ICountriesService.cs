namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using SightsView.Web.ViewModels.Countries;

    public interface ICountriesService
    {
        Task<IList<SelectListItem>> GetSelectListCountriesAsync();

        Task<IEnumerable<CountriesViewModel>> GetCountriesWithMostCreationAsync(int countriesCount);

        Task<string> GetCountryNameByIdAsync(int id);
    }
}
