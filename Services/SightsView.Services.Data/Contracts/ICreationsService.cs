namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    using SightsView.Data.Models;
    using SightsView.Web.ViewModels.Creations;
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

        Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations);

        Task<T> GetCreationByIdAsync<T>(string id);

        Task<bool> DeleteCreationAsync(string creationId, string userId);

        Task IncreseCreationViewsAsync(string creationId);

        Task<bool> EditCreationByIdAsync(string creationId, string title, string description, bool isPrivate, int? categoryId, int? countryId, string userId);
    }
}
