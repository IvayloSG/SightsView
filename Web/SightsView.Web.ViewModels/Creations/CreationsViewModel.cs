namespace SightsView.Web.ViewModels.Creations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CreationsViewModel
    {
        public string Id { get; set; }

        public string DataUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? Views { get; set; }

        public int? Likes { get; set; }

        public string CreatorId { get; set; }

        public string CreatorName { get; set; }
    }
}
