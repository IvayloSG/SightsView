namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using SightsView.Data.Models;
    using SightsView.Web.ViewModels.Tags;

    public interface ICreationsService
    {
        Task<string> AddCreationInDbAsync(
            string title,
            string description,
            bool isPrivate,
            int? countryId,
            int categoryId,
            ApplicationUser user,
            IFormFile creation,
            IEnumerable<TagsViewModel> tags);
    }
}
