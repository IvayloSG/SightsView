namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
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
            IEnumerable<TagsViewModel> tags,
            Cloudinary cloudinary);

        Task<IEnumerable<CreationsViewModel>> GetNumberRandomCreationsAsync(int countOfCreations);

        Task<CreationsViewModel> GetCreationByIdAsync(string id, string userId);
    }
}
