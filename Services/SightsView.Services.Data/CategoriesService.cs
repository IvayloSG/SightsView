namespace SightsView.Services.Data
{
    using System;
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
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoriesRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(string name, string description)
        {
            var category = new Category()
            {
                Name = name,
                Description = description,
            };

            var doesCategoryExist = await this.categoriesRepository.AllAsNoTracking()
                .AnyAsync(x => x.Name == name);

            if (doesCategoryExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.CategoryAlreadyExists, name));
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            var category = await this.categoriesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CategoryNotFound, id));
            }

            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllCategoriesAsync<T>()
        {
            var query = this.categoriesRepository.AllAsNoTracking();

            return await query.OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id)
            => await this.categoriesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<T> GetCategoryByNameAsync<T>(string name)
             => await this.categoriesRepository.AllAsNoTracking()
             .Where(x => x.Name == name)
             .To<T>()
             .FirstOrDefaultAsync();

        public async Task<IList<SelectListItem>> GetSelectListCategoriesAsync()
        {
            var selectListItems = await this.categoriesRepository
            .AllAsNoTracking()
            .OrderBy(x => x.Name)
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

        public async Task<IEnumerable<T>> GetTopCategoriesAsync<T>(int categoryCount)
            => await this.categoriesRepository.AllAsNoTracking()
            .OrderByDescending(x => x.Creations.Count())
            .Take(categoryCount)
            .To<T>()
            .ToListAsync();

        public async Task UpdateCategoryByIdAsync(int id, string name, string description)
        {
            var category = await this.categoriesRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                 throw new NullReferenceException(string.Format(
                    ExceptionMessages.CategoryNotFound, id));
            }

            category.Name = name;
            category.Description = description;

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
