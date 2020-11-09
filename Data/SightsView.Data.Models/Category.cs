namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;

    public class Category
    {
        public Category()
        {
            this.Creations = new HashSet<Creation>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(DataValidation.CategoryNameLength)]
        public string Name { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}