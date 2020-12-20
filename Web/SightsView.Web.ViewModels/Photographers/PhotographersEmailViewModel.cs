namespace SightsView.Web.ViewModels.Photographers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class PhotographersEmailViewModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
