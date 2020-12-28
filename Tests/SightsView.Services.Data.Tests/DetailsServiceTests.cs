namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Details;
    using Xunit;

    public class DetailsServiceTests : BaseServiceTests
    {
        private IDetailsService Service => this.ServiceProvider.GetRequiredService<IDetailsService>();

        [Fact]
        public async Task AddDetailsAsyncRestultTest()
        {
            var apereture = "TestApereture";
            var shutterSpeed = "TestShutterSpeed";
            var iso = "TestIso";
            var notes = "TestNotes";

            var result = await this.Service.AddDetailsAsync(apereture, shutterSpeed, iso, notes);

            var resultElement = await this.DbContext.Details.FirstOrDefaultAsync();

            Assert.Equal(result, resultElement.Id);
            Assert.Equal(apereture, resultElement.Apereture);
            Assert.Equal(shutterSpeed, resultElement.ShutterSpeed);
            Assert.Equal(iso, resultElement.ISO);
            Assert.Equal(notes, resultElement.TipAndTricks);
        }

        [Fact]
        public async Task GetDetailsByCreationIdResultTest()
        {
            var details = new Details()
            {
                Apereture = "TestApereture",
                ShutterSpeed = "TestShutterSpeed",
                ISO = "TestIso",
                TipAndTricks = "TestNotes",
                Creations = new List<Creation>() { new Creation() { Id = "13" } },
            };

            await this.DbContext.Details.AddAsync(details);
            await this.DbContext.SaveChangesAsync();

            var creationId = "13";

            var result = await this.Service.GetDetailsByCreationIdAsync<DetailsTestViewModel>(creationId);

            Assert.Equal(details.Id, result.Id);
            Assert.Equal(details.Apereture, result.Apereture);
        }

        [Fact]
        public async Task UpdateDetailsSuccessResulttest()
        {
            var details = new Details()
            {
                Apereture = "TestApereture",
                ShutterSpeed = "TestShutterSpeed",
                ISO = "TestIso",
                TipAndTricks = "TestNotes",
            };

            await this.DbContext.Details.AddAsync(details);
            await this.DbContext.SaveChangesAsync();

            var editedApereture = "EditedTestApereture";
            var editedShutterSpeed = "EditedTestSS";
            var editedIso = "EditedTestIso";
            var editedNotes = "EditedTestNotes";

            var result = await this.Service.UpdateDetailsAsync(details.Id, editedApereture, editedShutterSpeed, editedIso, editedNotes);
            var resultElement = await this.DbContext.Details.FirstOrDefaultAsync();

            Assert.True(result);
            Assert.Equal(editedApereture, resultElement.Apereture);
            Assert.Equal(editedShutterSpeed, resultElement.ShutterSpeed);
            Assert.Equal(editedIso, resultElement.ISO);
            Assert.Equal(editedNotes, resultElement.TipAndTricks);
        }

        [Fact]
        public async Task UpdateDetailsThrowsException()
        {
            var id = 7;
            string apereture = null;
            string shutterSpeed = null;
            string iso = null;
            string notes = null;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
                 () => this.Service.UpdateDetailsAsync(id, apereture, shutterSpeed, iso, notes));
        }
    }
}
