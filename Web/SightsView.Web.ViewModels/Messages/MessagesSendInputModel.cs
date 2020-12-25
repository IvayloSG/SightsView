namespace SightsView.Web.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class MessagesSendInputModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = GlobalConstants.CommentContentLengthError, MinimumLength = 2)]
        public string Content { get; set; }
    }
}
