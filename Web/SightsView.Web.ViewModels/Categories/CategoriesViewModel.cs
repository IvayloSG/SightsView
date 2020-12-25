namespace SightsView.Web.ViewModels.Categories
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
