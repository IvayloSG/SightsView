namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TagCreation
    {
        [Required]
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        [Required]
        public string CreationId { get; set; }

        public virtual Creation Creation { get; set; }
    }
}
