namespace SightsView.Web.ViewModels.Load
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SightsView.Web.ViewModels.Creations;

    public class LoadResponseModel
    {
        public IEnumerable<CreationsViewModel> Creations { get; set; }
    }
}
