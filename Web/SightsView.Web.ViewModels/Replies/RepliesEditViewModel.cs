namespace SightsView.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class RepliesEditViewModel : IMapFrom<Reply>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = GlobalConstants.RepliesContentLengthError, MinimumLength = 1)]
        public string Content { get; set; }
    }
}
