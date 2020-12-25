namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Categories;

    using Xunit;

    public class CategoriesServiceTests : BaseServiceTests
    {
        private ICategoriesService Service => this.ServiceProvider.GetRequiredService<ICategoriesService>();

        [Fact]
        public async Task CreateCategoryTest()
        {
            var name = "TestCategory";
            var description = "TestDescription";

            await this.Service.CreateCategoryAsync(name, description);

            var resultCount = await this.DbContext.Categories.CountAsync();
            var expectedCount = 1;

            Assert.Equal(expectedCount, resultCount);
        }

        [Fact]
        public async Task CreateCategoryResultTest()
        {
            var name = "TestCategory";
            var description = "TestDescription";

            await this.Service.CreateCategoryAsync(name, description);

            var category = await this.DbContext.Categories.FirstOrDefaultAsync();

            Assert.Equal(name, category.Name);
            Assert.Equal(description, category.Description);
        }

        [Fact]
        public async Task CreateCategoryThrowsExceptionForSameNAme()
        {
            var name = "TestCategory";
            var description = "TestDescription";

            await this.Service.CreateCategoryAsync(name, description);

            description = "AnotherTestDescription";

            var ex = await Assert.ThrowsAsync<ArgumentException>(
                () => this.Service.CreateCategoryAsync(name, description));
        }

        [Fact]
        public async Task DeleteCategoryRemovesRecordTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            var secondCategory = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.Categories.AddAsync(category);
            await this.DbContext.Categories.AddAsync(secondCategory);
            await this.DbContext.SaveChangesAsync();

            await this.Service.DeleteCategoryByIdAsync(category.Id);

            var resultCount = await this.DbContext.Categories.CountAsync();

            var expectedCount = 1;

            Assert.Equal(expectedCount, resultCount);
        }

        [Fact]
        public async Task DeleteCategoryThrowExceptionTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.Categories.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var id = 30;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
              () => this.Service.DeleteCategoryByIdAsync(id));
        }

        [Fact]
        public async Task GetAllCategoriesAsyncTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            var secondCategory = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.AddAsync(secondCategory);

            await this.DbContext.SaveChangesAsync();

            var categories = await this.Service.GetAllCategoriesAsync<CategoriesViewModel>();
            var categoryList = categories.ToList();

            var resultCategoryOne = categoryList[0];
            var resultCategoryTwo = categoryList[1];

            Assert.Equal(category.Name, resultCategoryOne.Name);
            Assert.Equal(category.Description, resultCategoryOne.Description);
            Assert.Equal(secondCategory.Name, resultCategoryTwo.Name);
            Assert.Equal(secondCategory.Description, resultCategoryTwo.Description);
        }

        [Fact]
        public async Task GetCategoryByIdResultTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCategoryByIdAsync<CategoriesViewModel>(category.Id);

            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
        }

        [Fact]
        public async Task GetCategoryByNameResultTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCategoryByNameAsync<CategoriesViewModel>(category.Name);

            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
        }

        [Fact]
        public async Task GetSelectListCategoriesResult()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            var secondCategory = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.AddAsync(secondCategory);

            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetSelectListCategoriesAsync();

            var expectedCount = 3;
            var firstElement = result[0];
            var expectedText = GlobalConstants.NoCategoryOption;
            var expectedValue = "0";

            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedText, firstElement.Text);
            Assert.Equal(expectedValue, firstElement.Value);
        }

        [Fact]
        public async Task GetTopCategoriesAsyncResultTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
                Creations = new List<Creation>() { new Creation() },
            };

            var secondCategory = new Category()
            {
                Name = "TestCategoryTwo",
                Description = "TestDescriptionTwo",
                Creations = new List<Creation>() { new Creation(), new Creation() },
            };

            var thirdCategory = new Category()
            {
                Name = "TestCategoryThree",
                Description = "TestDescriptionThree",
                Creations = new List<Creation>() { new Creation(), new Creation(), new Creation() },
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.AddAsync(secondCategory);
            await this.DbContext.AddAsync(thirdCategory);

            await this.DbContext.SaveChangesAsync();

            int countOfCategories = 2;
            var result = await this.Service.GetTopCategoriesAsync<CategoriesViewModel>(countOfCategories);

            var firstElement = result.ToList()[0];
            var resultCount = result.Count();

            Assert.Equal(countOfCategories, resultCount);
            Assert.Equal(thirdCategory.Name, firstElement.Name);
            Assert.Equal(thirdCategory.Description, firstElement.Description);
        }

        [Fact]
        public async Task UpdateCategoryResultTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var newName = "EditedTestCategory";
            var newDescription = "EditedTestCategory";

            await this.Service.UpdateCategoryByIdAsync(category.Id, newName, newDescription);

            var result = await this.DbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

            Assert.Equal(newName, result.Name);
            Assert.Equal(newDescription, result.Description);
        }

        [Fact]
        public async Task UpdateCategoryThrowsErrorTest()
        {
            var category = new Category()
            {
                Name = "TestCategory",
                Description = "TestDescription",
            };

            await this.DbContext.Categories.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var id = 30;
            var newName = "EditedTestCategory";
            var newDescription = "EditedTestCategory";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
              () => this.Service.UpdateCategoryByIdAsync(id, newName, newDescription));
        }
    }
}
