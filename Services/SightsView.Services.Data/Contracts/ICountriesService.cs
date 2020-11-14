namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICountriesService
    {
        Task<IList<SelectListItem>> GetSelectListCountriesAsync();
    }
}
