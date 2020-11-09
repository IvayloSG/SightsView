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
        public string MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
