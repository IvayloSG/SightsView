namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Reply : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        public string MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
