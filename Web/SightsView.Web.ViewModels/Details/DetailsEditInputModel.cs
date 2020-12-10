namespace SightsView.Web.ViewModels.Details
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class DetailsEditInputModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public int? DetailsId { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsAperetureLengthError, MinimumLength = 1)]
        public string DetailsApereture { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsShutterSteedLengthError, MinimumLength = 1)]
        public string DetailsShutterSpeed { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsIsoLengthError, MinimumLength = 1)]
        public string DetailsIso { get; set; }

        public string DetailsTipAndTricks { get; set; }

        // Placeholder properties
        public string PlaceholderAppereture
            => this.DetailsApereture ?? string.Empty;

        public string PlaceholderShutterSpeed
            => this.DetailsShutterSpeed ?? string.Empty;

        public string PlaceholderIso
            => this.DetailsIso ?? string.Empty;

        public string PlaceholderNotes
            => this.DetailsTipAndTricks ?? string.Empty;
    }
}
