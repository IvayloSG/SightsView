namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Creations;
    using SightsView.Web.ViewModels.Tags;
    using Xunit;

    public class CreationsServiceTests : BaseServiceTests
    {
        private ICreationsService Service => this.ServiceProvider.GetRequiredService<ICreationsService>();

        [Fact]
        public async Task AddCreationResultTest()
        {
            var title = "TestTitle";
            var description = "TestDescription";
            var isPrivate = false;
            var countryId = 1;
            var categoryId = 1;
            ApplicationUser user = new ApplicationUser() { UserName = "Pesho" };
            IFormFile inputCreation = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            List<TagsViewModel> tags = new List<TagsViewModel>() { new TagsViewModel() { Id = 1, Name = "test" }, new TagsViewModel() { Id = 2, Name = "testTwo" } };
            var isTest = true;

            var creationId = await this.Service.AddCreationAsync(title, description, isPrivate, countryId, categoryId, user, inputCreation, tags, isTest);
            var resultCreation = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creationId);
            var expectedTagsCount = 2;

            Assert.NotNull(creationId);
            Assert.False(resultCreation.IsPrivate);
            Assert.Equal(title, resultCreation.Title);
            Assert.Equal(description, resultCreation.Description);
            Assert.Equal(countryId, resultCreation.CountryId);
            Assert.Equal(categoryId, resultCreation.CategoryId);
            Assert.Equal(expectedTagsCount, resultCreation.Tags.Count);
        }

        [Fact]
        public async Task DeleteCreationAsyncSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.DeleteCreationAsync(creation.Id, creation.CreatorId);
            var resultCount = await this.DbContext.Creations.CountAsync();

            var expectedResult = true;
            var expectedCount = 0;

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedCount, resultCount);
        }

        [Fact]
        public async Task DeleteCreationAsyncInvalidUserResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            var creatorId = "31";

            var result = await this.Service.DeleteCreationAsync(creation.Id, creatorId);

            var expectedResult = false;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task DeleteCreationThrowsErrorTest()
        {
            var creationId = "7";
            var userId = "13";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
             () => this.Service.DeleteCreationAsync(creationId, userId));
        }

        [Fact]
        public async Task EditCreationByIdSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                Description = "TestDescription",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            var editedTitle = "EditedTestTitle";
            var editedDescription = "EditedTestDescription";
            var editedCategoryId = 5;
            var editedCountryId = 10;

            var result = await this.Service.EditCreationByIdAsync(creation.Id, editedTitle, editedDescription, creation.IsPrivate, editedCategoryId, editedCountryId, creation.CreatorId);
            var resultCreation = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creation.Id);

            Assert.True(result);
            Assert.False(resultCreation.IsPrivate);
            Assert.Equal(editedTitle, resultCreation.Title);
            Assert.Equal(editedDescription, resultCreation.Description);
            Assert.Equal(editedCategoryId, resultCreation.CategoryId);
            Assert.Equal(editedCountryId, resultCreation.CountryId);
        }

        [Fact]
        public async Task EditCreationByIdFalseResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                Description = "TestDescription",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            var editedTitle = "EditedTestTitle";
            var editedDescription = "EditedTestDescription";
            var editedCategoryId = 5;
            var editedCountryId = 10;
            var creatorId = "66";

            var result = await this.Service.EditCreationByIdAsync(creation.Id, editedTitle, editedDescription, creation.IsPrivate, editedCategoryId, editedCountryId, creatorId);
            var resultCreation = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creation.Id);

            Assert.False(result);
            Assert.False(resultCreation.IsPrivate);
            Assert.Equal(creation.Title, resultCreation.Title);
        }

        [Fact]
        public async Task EditCreationByIdThrowsException()
        {
            var creationId = "700";
            var editedTitle = "EditedTestTitle";
            var editedDescription = "EditedTestDescription";
            var editedCategoryId = 5;
            var editedCountryId = 10;
            var creatorId = "13";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
           () => this.Service.EditCreationByIdAsync(creationId, editedTitle, editedDescription, true, editedCategoryId, editedCountryId, creatorId));
        }

        [Fact]
        public async Task GetCreationModelByIdResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                StorageAddress = "TestAddress",
                CreatorId = "13",
                CreationDataUrl = "TestUrl",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCreationModelByIdAsync<CreationsTestViewModel>(creation.Id);

            Assert.Equal(creation.Id, result.Id);
            Assert.Equal(creation.CreatorId, result.CreatorId);
            Assert.Equal(creation.StorageAddress, result.StorageAddress);
            Assert.Equal(creation.CreationDataUrl, result.CreationDataUrl);
        }

        [Fact]
        public async Task IncreseCreationViewsSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                Description = "TestDescription",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                StorageAddress = "TestAddress",
                Views = 7,
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.SaveChangesAsync();

            await this.Service.IncreseCreationViewsAsync(creation.Id);

            var result = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creation.Id);

            var expectedViews = 8;

            Assert.Equal(expectedViews, result.Views);
            Assert.Equal(creation.Id, result.Id);
        }

        [Fact]
        public async Task IncreseCreationViewsThrowsExceptionTest()
        {
            var creationId = "600";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
          () => this.Service.IncreseCreationViewsAsync(creationId));
        }
    }
}
