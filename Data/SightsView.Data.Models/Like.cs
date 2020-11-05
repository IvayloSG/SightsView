namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Like
    {
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string CreationId { get; set; }

        public virtual Creation Creation { get; set; }
    }
}