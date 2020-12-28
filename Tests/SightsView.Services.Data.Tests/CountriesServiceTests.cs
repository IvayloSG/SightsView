namespace SightsView.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using Xunit;

    public class CountriesServiceTests : BaseServiceTests
    {
        private ICountriesService Service => this.ServiceProvider.GetRequiredService<ICountriesService>();

        [Fact]
        public async Task GetCountriesWithMostCreationResultTest()
        {
            var country = new Country()
            {
                Name = "TestName",
                Creations = new List<Creation>() { new Creation() },
            };

            var countryTwo = new Country()
            {
                Name = "TestNameTwo",
                Creations = new List<Creation>() { new Creation(), new Creation() },
            };

            var countryThree = new Country()
            {
                Name = "TestNameTwo",
                Creations = new List<Creation>() { new Creation(), new Creation() },
            };

            await this.DbContext.Countries.AddAsync(country);
            await this.DbContext.Countries.AddAsync(countryTwo);
            await this.DbContext.SaveChangesAsync();

            var count = 2;

            var result = await this.Service.GetCountriesWithMostCreationAsync(count);
            var resultList = result.ToList();
            var resultElement = resultList[0];

            Assert.Equal(count, result.Count());
            Assert.Equal(countryTwo.Name, resultElement.Name);
        }

        [Fact]
        public async Task GetCountryNameByIdNullResultTest()
        {
            var country = new Country()
            {
                Name = "TestName",
            };

            await this.DbContext.Countries.AddAsync(country);
            await this.DbContext.SaveChangesAsync();

            var id = 7;

            var result = await this.Service.GetCountryNameByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCountryNameByIdSuccessResultTest()
        {
            var country = new Country()
            {
                Name = "TestName",
            };

            await this.DbContext.Countries.AddAsync(country);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCountryNameByIdAsync(country.Id);

            Assert.Equal(country.Name, result);
        }

        [Fact]
        public async Task GetSelectListCountriesResult()
        {
            var country = new Country()
            {
                Name = "TestName",
                Creations = new List<Creation>() { new Creation() },
            };

            var countryTwo = new Country()
            {
                Name = "TestNameTwo",
                Creations = new List<Creation>() { new Creation(), new Creation() },
            };

            await this.DbContext.AddAsync(country);
            await this.DbContext.AddAsync(countryTwo);

            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetSelectListCountriesAsync();

            var expectedCount = 3;
            var firstElement = result[0];
            var expectedText = GlobalConstants.NoCountryOption;
            var expectedValue = "0";

            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(expectedText, firstElement.Text);
            Assert.Equal(expectedValue, firstElement.Value);
        }
    }
}
