namespace SightsView.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class RepliesAddInputModel
    {
        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstants.RepliesContentLengthError, MinimumLength = 1)]
        public string Content { get; set; }

        public int CommentId { get; set; }

        public string ApplicationUserId { get; set; }


    }
}
