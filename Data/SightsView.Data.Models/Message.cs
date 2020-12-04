namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        [Required]
        [MaxLength(DataValidation.MessageContentLength)]
        public string Content { get; set; }
    }
}
