namespace SightsView.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using SightsView.Web.ViewModels.Countries;
    using SightsView.Web.ViewModels.Creations;

    public class HomeIndexViewModel
    {
        public IEnumerable<CountriesViewModel> CountriesWithMostImages { get; set; }

        public IEnumerable<CreationsViewModel> Creations { get; set; }
    }
}
