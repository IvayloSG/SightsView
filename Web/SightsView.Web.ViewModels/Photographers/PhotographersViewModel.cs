namespace SightsView.Web.ViewModels.Photographers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PhotographersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public int Creations { get; set; }

        public int LikedCreations { get; set; }

        public int Followers { get; set; }

        public int Reputation
            => this.Creations + this.Followers + this.LikedCreations;
    }
}
