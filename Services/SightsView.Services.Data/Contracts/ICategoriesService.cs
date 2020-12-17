namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAllCategoriesAsync<T>();

        Task CreateCategoryAsync(string name, string description);

        Task<T> GetCategoryByIdAsync<T>(int id);

        Task<T> GetCategoryByNameAsync<T>(string name);

        Task<IEnumerable<T>> GetTopCategoriesAsync<T>(int categoryCount);

        Task<bool> UpdateCategoryByIdAsync(int id, string name, string description);

        Task<bool> DeleteCategoryByIdAsync(int id);

        Task<IList<SelectListItem>> GetSelectListCategoriesAsync();

        Task<IEnumerable<T>> GetCategoriesWithMostCreationsAsync<T>(int countOfCategories);
    }
}
