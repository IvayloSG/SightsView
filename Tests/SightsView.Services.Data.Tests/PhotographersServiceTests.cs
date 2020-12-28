namespace SightsView.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Photographers;
    using Xunit;

    public class PhotographersServiceTests : BaseServiceTests
    {
        private IPhotographersService Service => this.ServiceProvider.GetRequiredService<IPhotographersService>();

        [Fact]
        public async Task GetAllPhotographersResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.SaveChangesAsync();

            var userId = "1";

            var result = await this.Service.GetAllPhotographersAsync(userId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(photographer.Id, resultFirstElement.Id);
            Assert.Equal(photographer.UserName, resultFirstElement.Username);
        }

        [Fact]
        public async Task GetPhotographersWithMostCreationsResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>() { new Creation() },
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.SaveChangesAsync();

            var userId = "1";

            var result = await this.Service.GetPhotographersWithMostCreationsAsync(userId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(photographerTwo.Id, resultFirstElement.Id);
            Assert.Equal(photographerTwo.UserName, resultFirstElement.Username);
        }

        [Fact]
        public async Task GetPhotographersWithMostLikesResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>() { new Creation() },
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.SaveChangesAsync();

            var userId = "1";

            var result = await this.Service.GetPhotographersWithMostLikesAsync(userId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(photographer.Id, resultFirstElement.Id);
            Assert.Equal(photographer.UserName, resultFirstElement.Username);
        }

        [Fact]
        public async Task GetPhotographersWithMostFollowersResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>() { new Creation() },
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFive = new ApplicationUser()
            {
                Id = "9",
                UserName = "TestFive",
                CountryId = 2,
                Followers = new List<Follow>() { new Follow() },
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.Users.AddAsync(photographerFive);
            await this.DbContext.SaveChangesAsync();

            var userId = "1";

            var result = await this.Service.GetPhotographersWithMostFollowersAsync(userId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 3;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(photographerFive.Id, resultFirstElement.Id);
            Assert.Equal(photographerFive.UserName, resultFirstElement.Username);
        }

        [Fact]
        public async Task GetPhotographersNewestResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>() { new Creation() },
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.SaveChangesAsync();

            var userId = "1";

            var result = await this.Service.GetPhotographersMostNewestAsync(userId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(photographerTwo.Id, resultFirstElement.Id);
            Assert.Equal(photographerTwo.UserName, resultFirstElement.Username);
        }

        [Fact]
        public async Task GetPhotographerByIdResultTest()
        {
            var photographer = new ApplicationUser()
            {
                Id = "7",
                UserName = "TestUser",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerTwo = new ApplicationUser()
            {
                Id = "8",
                UserName = "TestUserTwo",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>() { new Creation() },
            };

            var photographerThree = new ApplicationUser()
            {
                Id = "1",
                UserName = "TestUserThree",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            var photographerFour = new ApplicationUser()
            {
                Id = "3",
                UserName = "admin",
                CountryId = 2,
                Followers = new List<Follow>(),
                Creations = new List<Creation>(),
            };

            await this.DbContext.Users.AddAsync(photographer);
            await this.DbContext.Users.AddAsync(photographerTwo);
            await this.DbContext.Users.AddAsync(photographerThree);
            await this.DbContext.Users.AddAsync(photographerFour);
            await this.DbContext.SaveChangesAsync();

            var userId = "8";

            var result = await this.Service.GetPhotographerByIdAsync<PhotographersEmailViewModel>(userId);

            Assert.Equal(photographerTwo.UserName, result.UserName);
        }
    }
}
