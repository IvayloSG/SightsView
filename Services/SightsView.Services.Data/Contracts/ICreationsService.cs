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
        Task<string> AddCreationAsync(
            string title,
            string description,
            bool isPrivate,
            int? countryId,
            int categoryId,
            ApplicationUser user,
            IFormFile creation,
            IEnumerable<TagsViewModel> tags,
            bool isTest = false);

        Task AddDetailsToCreationAsync(string creationId, int detailsId);

        Task AddEquipmentToCreationAsync(string creationId, int equipmentId);

        Task<bool> DeleteCreationAsync(string creationId, string userId);

        Task<bool> EditCreationByIdAsync(string creationId, string title, string description, bool isPrivate, int? categoryId, int? countryId, string userId);

        Task<IEnumerable<T>> GetCreationByCountryAsync<T>(int countryId, int pageNumber, int creationsCount);

        Task<IEnumerable<T>> GetCreationsByNameOrTagAsync<T>(string keyWord, int creationsCount);

        Task<IEnumerable<T>> GetCreationsByCreatorIdAsync<T>(string creatorId, int pageNumber, int creationsCount);

        Task<IEnumerable<T>> GetCreationsIncudingPrivateByCreatorIdAsync<T>(string creatorId, int pageNumber, int creationsCount);

        Task<string> GetCreatorIdByCreationIdAsync(string creationId);

        Task<T> GetCreationModelByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetNewestCreationsByCategoryAsync<T>(int? categoryId, int pageNumber, int creationsCount);

        Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations);

        Task IncreseCreationViewsAsync(string creationId);
    }
}
