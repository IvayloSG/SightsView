namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(DataValidation.ReplyContentLength)]
        public string Content { get; set; }

        [Required]
        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
