namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Creations = new HashSet<Creation>();
        }

        [Required]
        [MaxLength(DataValidation.CategoryNameLength)]
        public string Name { get; set; }

        [MaxLength(DataValidation.CategoryDescriptionLength)]
        public string Description { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}