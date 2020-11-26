namespace SightsView.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using SightsView.Web.ViewModels.Countries;
    using SightsView.Web.ViewModels.Creations;

    public class HomeIndexViewModel
    {
        public IEnumerable<CountriesViewModel> CountriesWithMostImages { get; set; }

        // TODO: Change ViewModel not to have more properties that shown
        public IEnumerable<CreationsViewModel> Creations { get; set; }
    }
}
