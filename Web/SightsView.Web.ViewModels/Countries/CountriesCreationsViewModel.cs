namespace SightsView.Web.ViewModels.Countries
{
    using System.Collections.Generic;

    using SightsView.Web.ViewModels.Creations;

    public class CountriesCreationsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<CreationsViewModel> Creations { get; set; }
    }
}
