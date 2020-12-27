namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SightsView.Web.ViewModels.Photographers;

    public interface IPhotographersService
    {
        Task<IEnumerable<PhotographersViewModel>> GetAllPhotographersAsync(string currentUserId);

        Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostCreationsAsync(string currentUserId);

        Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostLikesAsync(string currentUserId);

        Task<IEnumerable<PhotographersViewModel>> GetPhotographersWithMostFollowersAsync(string currentUserId);

        Task<IEnumerable<PhotographersViewModel>> GetPhotographersMostNewestAsync(string currentUserId);

        Task<T> GetPhotographerByIdAsync<T>(string photographerId);
    }
}
