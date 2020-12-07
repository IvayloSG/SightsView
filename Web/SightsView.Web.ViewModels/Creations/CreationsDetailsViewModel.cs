namespace SightsView.Web.ViewModels.Creations
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsDetailsViewModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public int? DetailsId { get; set; }

        public string DetailsAppereture { get; set; }

        public string DetailsShutterSpeed { get; set; }

        public string DetailsIso { get; set; }

        public string DetailsTipsAndTricks { get; set; }


        // Rendering Properties
        public string ApperetureView
            => this.DetailsAppereture == null ? "N/A" : this.DetailsAppereture;

        public string ShutterSpeedView
            => this.DetailsShutterSpeed == null ? "N/A" : this.DetailsShutterSpeed;

        public string IsoView
           => this.DetailsIso == null ? "N/A" : this.DetailsIso;

        public string NotesView
           => this.DetailsTipsAndTricks == null ? string.Empty : this.DetailsTipsAndTricks;
    }
}
