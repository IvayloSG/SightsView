namespace SightsView.Web.ViewModels.Creations
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsDetailsViewModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        public int? DetailsId { get; set; }

        public string DetailsApereture { get; set; }

        public string DetailsShutterSpeed { get; set; }

        public string DetailsIso { get; set; }

        public string DetailsTipAndTricks { get; set; }

        // Rendering Properties
        public string AperetureView
            => this.DetailsApereture ?? "N/A";

        public string ShutterSpeedView
            => this.DetailsShutterSpeed ?? "N/A";

        public string IsoView
           => this.DetailsIso ?? "N/A";

        public string NotesView
           => this.DetailsTipAndTricks ?? string.Empty;
    }
}
