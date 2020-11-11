namespace SightsView.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string CreationId { get; set; }

        public virtual Creation Creation { get; set; }

        [Required]
        [MaxLength(DataValidation.CommentContentLength)]
        public string Content { get; set; }
    }
}