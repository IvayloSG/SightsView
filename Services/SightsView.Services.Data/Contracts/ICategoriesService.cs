namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        Task<IEnumerable<T>> GetAllCategoriesAsync<T>();

        Task CreateCategoryAsync(string name, string description);

        Task<T> GetCategoryByIdAsync<T>(int id);

        Task<bool> UpdateCategoryByIdAsync(int id, string name, string description);

        Task<bool> DeleteCategoryByIdAsync(int id);
    }
}
