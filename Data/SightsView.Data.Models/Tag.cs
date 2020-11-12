namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Creations = new HashSet<TagCreation>();
        }

        [Required]
        [MaxLength(DataValidation.TagNameLength)]
        public string Name { get; set; }

        public virtual ICollection<TagCreation> Creations { get; set; }
    }
}