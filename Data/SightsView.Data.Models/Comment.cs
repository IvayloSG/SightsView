namespace SightsView.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string CreationId { get; set; }

        public virtual Creation Creation { get; set; }

        [Required]
        [MaxLength(255)]
        public string Content { get; set; }
    }
}