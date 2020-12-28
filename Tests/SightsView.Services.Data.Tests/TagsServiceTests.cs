namespace SightsView.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Services.Data.Contracts;
    using Xunit;

    public class TagsServiceTests : BaseServiceTests
    {
        private ITagsService Service => this.ServiceProvider.GetRequiredService<ITagsService>();

        [Fact]
        public async Task CreateTagsAsyncResultTests()
        {
            List<string> tagNames = new List<string>() { "test", "test1", "test2" };

            await this.Service.CreateTagsAsync(tagNames);

            var result = await this.DbContext.Tags.FirstOrDefaultAsync();
            var resultCount = await this.DbContext.Tags.CountAsync();

            var expectedCount = 3;

            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(result.Name, tagNames[0]);
        }

        [Fact]
        public async Task GetTagsByNameResultTest()
        {
            List<string> tagNames = new List<string>() { "test", "test1", "test2" };

            await this.Service.CreateTagsAsync(tagNames);

            List<string> searchNames = new List<string>() { "test", "test1" };

            var result = await this.Service.GetTagsByNameAsync(searchNames);
            var resultList = result.ToList();

            var expectedCount = 2;

            Assert.Equal(expectedCount, resultList.Count);
        }
    }
}
