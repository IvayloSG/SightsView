namespace SightsView.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class CommentsAddInputModel
    {
        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstants.CreationDescriptionLengthError, MinimumLength = 1)]
        public string Content { get; set; }

        public string CreationId { get; set; }
    }
}
