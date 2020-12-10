namespace SightsView.Web.ViewModels.Details
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class DetailsAddInputModel
    {
        public string CreationId { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsAperetureLengthError, MinimumLength = 1)]
        public string Apereture { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsShutterSteedLengthError, MinimumLength = 1)]
        public string ShutterSpeed { get; set; }

        [StringLength(20, ErrorMessage = GlobalConstants.DetailsIsoLengthError, MinimumLength = 1)]
        public string Iso { get; set; }

        public string Notes { get; set; }
    }
}
