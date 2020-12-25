namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using Xunit;

    public class EquipmentServiceTests : BaseServiceTests
    {
        private IEquipmentsService Service => this.ServiceProvider.GetRequiredService<IEquipmentsService>();

        [Fact]
        public async Task AddEquipmentAsyncRestultTest()
        {
            var brand = "TestBrand";
            var model = "TestModel";
            var accessoaries = "TestAccessoaries";
            var notes = "TestNotes";

            var result = await this.Service.AddEquipmentAsync(brand, model, accessoaries, notes);

            var resultElement = await this.DbContext.Equipment.FirstOrDefaultAsync();

            Assert.Equal(result, resultElement.Id);
            Assert.Equal(brand, resultElement.Brand);
            Assert.Equal(model, resultElement.Model);
            Assert.Equal(accessoaries, resultElement.Accessoaries);
            Assert.Equal(notes, resultElement.Notes);
        }

        [Fact]
        public async Task UpdateDetailsSuccessResulttest()
        {
            var equipment = new Equipment()
            {
                Brand = "TestBrand",
                Model = "TestModel",
                Accessoaries = "TestAcessoaries",
                Notes = "TestNotes",
            };

            await this.DbContext.Equipment.AddAsync(equipment);
            await this.DbContext.SaveChangesAsync();

            var editedBrand = "EditedTestBarnd";
            var editedModel = "EditedTestModel";
            var editedAccessoaries = "EditedTestAccessoaries";
            var editedNotes = "EditedTestNotes";

            var result = await this.Service.UpdateEquipmentAsync(equipment.Id, editedBrand, editedModel, editedAccessoaries, editedNotes);
            var resultElement = await this.DbContext.Equipment.FirstOrDefaultAsync();

            Assert.True(result);
            Assert.Equal(editedBrand, resultElement.Brand);
            Assert.Equal(editedModel, resultElement.Model);
            Assert.Equal(editedAccessoaries, resultElement.Accessoaries);
            Assert.Equal(editedNotes, resultElement.Notes);
        }

        [Fact]
        public async Task UpdateDetailsThrowsException()
        {
            var id = 7;
            string brand = null;
            string model = null;
            string accessoaries = null;
            string notes = null;

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
                 () => this.Service.UpdateEquipmentAsync(id, brand, model, accessoaries, notes));
        }
    }
}
