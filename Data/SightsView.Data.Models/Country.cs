namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Creations = new HashSet<Creation>();
            this.Creators = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(DataValidation.CountryNameLength)]
        public string Name { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }

        public virtual ICollection<ApplicationUser> Creators { get; set; }
    }
}