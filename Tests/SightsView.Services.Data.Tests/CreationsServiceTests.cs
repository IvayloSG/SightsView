namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
        public async Task AddDetailsToCreationSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            var details = new Details()
            {
                Apereture = "2.8",
                ShutterSpeed = "1/200",
                ISO = "200",
                TipAndTricks = "Test",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Details.AddAsync(details);
            await this.DbContext.SaveChangesAsync();

            await this.Service.AddDetailsToCreationAsync(creation.Id, details.Id);

            var result = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creation.Id);

            Assert.Equal(details.Id, result.DetailsId);
        }

        [Fact]
        public async Task AddDetailsToCreationThrowsExceptionTest()
        {
            var creattionId = "700";
            var detailsId = 700;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
           () => this.Service.AddDetailsToCreationAsync(creattionId, detailsId));
        }

        [Fact]
        public async Task AddEquipmentToCreationSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                StorageAddress = "TestAddress",
                CreatorId = "13",
            };

            var equipment = new Equipment()
            {
                Brand = "Nikon",
                Model = "Z6",
                Accessoaries = "Test",
                Notes = "Test",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Equipment.AddAsync(equipment);
            await this.DbContext.SaveChangesAsync();

            await this.Service.AddEquipmentToCreationAsync(creation.Id, equipment.Id);

            var result = await this.DbContext.Creations.FirstOrDefaultAsync(x => x.Id == creation.Id);

            Assert.Equal(equipment.Id, result.EquipmentId);
        }

        [Fact]
        public async Task AddEquipmentToCreationThrowsExceptionTest()
        {
            var creattionId = "700";
            var equipmentId = 700;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
           () => this.Service.AddEquipmentToCreationAsync(creattionId, equipmentId));
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
        public async Task GetCreationByCountryAsyncResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "TestCreationTwo",
                IsPrivate = true,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationThree = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 3,
                CountryId = 2,
                CreatorId = "13",
            };

            var creationFour = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 3,
                CountryId = 1,
                CreatorId = "13",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.Creations.AddAsync(creationThree);
            await this.DbContext.Creations.AddAsync(creationFour);
            await this.DbContext.SaveChangesAsync();

            var countryId = 1;
            var pageNumber = 1;
            var creationsCount = 3;

            var result = await this.Service.GetCreationByCountryAsync<CreationsTestViewModel>(countryId, pageNumber, creationsCount);
            var resultList = result.ToList();
            var expectedFirstElement = resultList[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(creationFour.CreatorId, expectedFirstElement.CreatorId);
            Assert.Equal(creationFour.CategoryId, expectedFirstElement.CategoryId);
        }

        [Fact]
        public async Task GetCreationByCountryAsyncPaginationTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "TestCreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "15",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.SaveChangesAsync();

            var countryId = 1;
            var pageNumber = 2;
            var creationsCount = 1;

            var result = await this.Service.GetCreationByCountryAsync<CreationsTestViewModel>(countryId, pageNumber, creationsCount);
            var resultElement = result.FirstOrDefault();

            var expectedCount = 1;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(creation.CreatorId, resultElement.CreatorId);
            Assert.Equal(creation.CategoryId, resultElement.CategoryId);
        }

        [Fact]
        public async Task GetCreationsByNameOrTagResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "CreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "15",
            };

            var creationThree = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "17",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.Creations.AddAsync(creationThree);
            await this.DbContext.SaveChangesAsync();

            var keyWord = "Test";
            var count = 2;

            var result = await this.Service.GetCreationsByNameOrTagAsync<CreationsTestViewModel>(keyWord, count);
            var resultList = result.ToList();
            var resultFirstElement = resultList[0];

            Assert.Equal(count, result.Count());
            Assert.Equal(creation.CategoryId, resultFirstElement.CategoryId);
        }

        [Fact]
        public async Task GetCreatorIdByCreationIdSuccessResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "TestCreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "15",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCreatorIdByCreationIdAsync(creationTwo.Id);

            Assert.Equal(creationTwo.CreatorId, result);
        }

        [Fact]
        public async Task GetCreatorIdByCreationIdThrowsError()
        {
            var creationId = "15";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
         () => this.Service.GetCreatorIdByCreationIdAsync(creationId));
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
        public async Task GetNumberRandomCreationsResultTest()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "CreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "15",
            };

            var creationThree = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "17",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.Creations.AddAsync(creationThree);
            await this.DbContext.SaveChangesAsync();

            var creationsCount = 2;

            var result = await this.Service.GetNumberRandomCreationsAsync(creationsCount);

            Assert.Equal(creationsCount, result.Count());
        }

        [Fact]
        public async Task GetNewestCreationsByCategoryNullCategoryResult()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "CreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 2,
                CreatorId = "15",
            };

            var creationThree = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 3,
                CreatorId = "17",
            };

            var creationFour = new Creation()
            {
                Title = "TestCreationFour",
                IsPrivate = true,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "17",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.Creations.AddAsync(creationThree);
            await this.DbContext.SaveChangesAsync();

            int? categoryId = null;
            var pageNumber = 1;
            var pageSize = 5;

            var result = await this.Service.GetNewestCreationsByCategoryAsync<CreationsTestViewModel>(categoryId, pageNumber, pageSize);
            var resultList = result.ToList();
            var resultFirstElement = resultList[0];

            var expectedCount = 3;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(creationThree.CreatorId, resultFirstElement.CreatorId);
            Assert.Equal(creationThree.CountryId, resultFirstElement.CountryId);
        }

        [Fact]
        public async Task GetNewestCreationsByCategorySpecificCategoryResult()
        {
            var creation = new Creation()
            {
                Title = "TestCreation",
                IsPrivate = false,
                CategoryId = 1,
                CountryId = 1,
                CreatorId = "13",
            };

            var creationTwo = new Creation()
            {
                Title = "CreationTwo",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 2,
                CreatorId = "15",
            };

            var creationThree = new Creation()
            {
                Title = "TestCreationThree",
                IsPrivate = false,
                CategoryId = 2,
                CountryId = 3,
                CreatorId = "17",
            };

            var creationFour = new Creation()
            {
                Title = "TestCreationFour",
                IsPrivate = true,
                CategoryId = 2,
                CountryId = 1,
                CreatorId = "17",
            };

            await this.DbContext.Creations.AddAsync(creation);
            await this.DbContext.Creations.AddAsync(creationTwo);
            await this.DbContext.Creations.AddAsync(creationThree);
            await this.DbContext.SaveChangesAsync();

            int? categoryId = 2;
            var pageNumber = 1;
            var pageSize = 5;

            var result = await this.Service.GetNewestCreationsByCategoryAsync<CreationsTestViewModel>(categoryId, pageNumber, pageSize);
            var resultList = result.ToList();
            var resultFirstElement = resultList[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(creationThree.CreatorId, resultFirstElement.CreatorId);
            Assert.Equal(creationThree.CountryId, resultFirstElement.CountryId);
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
