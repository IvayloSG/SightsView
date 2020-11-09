namespace SightsView.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore.Internal;

    using SightsView.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var allCategories = new List<string>
            {
                "Abstract",
                "Aerial",
                "Architectural",
                "Beauty",
                "Bird",
                "Black and White",
                "Bodyshape",
                "Candid",
                "Conceptual",
                "Event",
                "Family",
                "Fashion",
                "Fireworks",
                "Food",
                "Forced Perspective",
                "HDR",
                "High Speed",
                "Infra Red",
                "Landscape",
                "Long Exposure",
                "Lomo",
                "Macro",
                "Micro",
                "Mobile",
                "Nature",
                "Night",
                "Nude",
                "Other",
                "Pet",
                "Portrait",
                "Real Estate",
                "Sports",
                "Storm",
                "Street",
                "Time Lapse",
                "Travel",
                "Underwater",
                "Vehicle",
                "Vintage",
                "Wedding",
                "Wildlife",
            };

            var categoriesModel = allCategories.Select(c => new Category() { Name = c });
            await dbContext.Categories.AddRangeAsync(categoriesModel);
        }
    }
}
