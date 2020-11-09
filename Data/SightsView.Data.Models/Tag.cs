namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;

    public class Tag
    {
        public Tag()
        {
            this.Creations = new HashSet<TagCreation>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.TagNameLength)]
        public string Name { get; set; }

        public virtual ICollection<TagCreation> Creations { get; set; }
    }
}