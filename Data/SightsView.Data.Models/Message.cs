namespace SightsView.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public Message()
        {
            this.Replies = new HashSet<Reply>();
        }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        [Required]
        [MaxLength(DataValidation.MessageContentLength)]
        public string Content { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
