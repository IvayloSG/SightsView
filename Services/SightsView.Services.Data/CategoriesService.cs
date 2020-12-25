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
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(string name, string description)
        {
            var category = new Category()
            {
                Name = name,
                Description = description,
            };

            var doesCategoryExist = await this.categoryRepository.AllAsNoTracking()
                .AnyAsync(x => x.Name == name);

            if (doesCategoryExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.CategoryAlreadyExists, name));
            }

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteCategoryByIdAsync(int id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                throw new NullReferenceException(string.Format(
                    ExceptionMessages.CategoryNotFound, id));
            }

            this.categoryRepository.Delete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllCategoriesAsync<T>()
        {
            var query = this.categoryRepository.AllAsNoTracking();

            return await query.OrderBy(x => x.Name)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id)
            => await this.categoryRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<T> GetCategoryByNameAsync<T>(string name)
             => await this.categoryRepository.AllAsNoTracking()
             .Where(x => x.Name == name)
             .To<T>()
             .FirstOrDefaultAsync();

        public async Task<IList<SelectListItem>> GetSelectListCategoriesAsync()
        {
            var selectListItems = await this.categoryRepository
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
            => await this.categoryRepository.AllAsNoTracking()
            .OrderByDescending(x => x.Creations.Count())
            .Take(categoryCount)
            .To<T>()
            .ToListAsync();

        public async Task UpdateCategoryByIdAsync(int id, string name, string description)
        {
            var category = await this.categoryRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                 throw new NullReferenceException(string.Format(
                    ExceptionMessages.CategoryNotFound, id));
            }

            category.Name = name;
            category.Description = description;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
