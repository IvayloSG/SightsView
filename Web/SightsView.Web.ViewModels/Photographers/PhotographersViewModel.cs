namespace SightsView.Web.ViewModels.Photographers
{
    using System.Collections.Generic;

    public class PhotographersViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Country { get; set; }

        public int CreationsCount { get; set; }

        public int LikedCreations { get; set; }

        public int Followers { get; set; }

        public int Reputation
            => this.CreationsCount + this.Followers + this.LikedCreations;

        public string BestCreationId { get; set; }

        public string BestCreationsUrl { get; set; }

        public string RunnerupCreationsId { get; set; }

        public string RunnerupCreationsUrl { get; set; }
    }
}
