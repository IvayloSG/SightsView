namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(30)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        [MaxLength(200)]
        public string Accessoaries { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}