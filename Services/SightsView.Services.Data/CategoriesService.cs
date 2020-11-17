namespace SightsView.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    using SightsView.Common;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<T>> GetAllCategoriesAsync<T>()
        {
            var query = this.categoryRepository.AllAsNoTracking();

            return await query.OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task CreateCategoryAsync(string name, string description)
        {
            var category = new Category()
            {
                Name = name,
                Description = description,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id)
            => await this.categoryRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<bool> UpdateCategoryByIdAsync(int id, string name, string description)
        {
            var category = await this.categoryRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return false;
            }

            category.Name = name;
            category.Description = description;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id)
        {
            var category = await this.categoryRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return false;
            }

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IList<SelectListItem>> GetSelectListCategoriesAsync()
        {
            var selectListItems = await this.categoryRepository
            .AllAsNoTracking()
            .OrderBy(X => X.Name)
            .Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            })
            .ToListAsync();

            var noCategoryOption = new SelectListItem(GlobalConstants.NoCategoryOption, 0.ToString());

            selectListItems.Insert(0, noCategoryOption);

            return selectListItems;
        }

        public async Task<T> GetCategoryByNameAsync<T>(string name)
            => await this.categoryRepository.AllAsNoTracking()
            .Where(x => x.Name == name)
            .To<T>()
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetCategoriesWithMostCreationsAsync<T>(int countOfCategories)
            => await this.categoryRepository.AllAsNoTracking()
            .OrderByDescending(x => x.Creations.Count)
            .To<T>()
            .Take(countOfCategories)
            .ToListAsync();
    }
}
