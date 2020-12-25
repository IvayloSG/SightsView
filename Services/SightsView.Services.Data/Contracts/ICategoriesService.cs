namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        Task CreateCategoryAsync(string name, string description);

        Task DeleteCategoryByIdAsync(int id);

        Task<IEnumerable<T>> GetAllCategoriesAsync<T>();

        Task<T> GetCategoryByIdAsync<T>(int id);

        Task<T> GetCategoryByNameAsync<T>(string name);

        Task<IList<SelectListItem>> GetSelectListCategoriesAsync();

        Task<IEnumerable<T>> GetTopCategoriesAsync<T>(int categoryCount);

        Task UpdateCategoryByIdAsync(int id, string name, string description);
    }
}
