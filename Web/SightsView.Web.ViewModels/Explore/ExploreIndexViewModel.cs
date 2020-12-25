namespace SightsView.Web.ViewModels.Explore
{
    using System.Collections.Generic;

    using SightsView.Web.ViewModels.Categories;
    using SightsView.Web.ViewModels.Creations;

    public class ExploreIndexViewModel
    {
        public int Id { get; set; }

        public string SearchInput { get; set; }

        public IEnumerable<CategoriesViewModel> TopCategories { get; set; }

        public IEnumerable<CreationsViewModel> Creations { get; set; }
    }
}
