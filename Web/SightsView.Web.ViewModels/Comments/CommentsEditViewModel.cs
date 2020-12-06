namespace SightsView.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CommentsEditViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstants.CommentContentLengthError, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
