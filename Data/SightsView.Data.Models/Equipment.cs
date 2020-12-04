namespace SightsView.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common;
    using SightsView.Data.Common.Models;

    public class Equipment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(DataValidation.EquipmentBrandLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(DataValidation.EquipmentModelLength)]
        public string Model { get; set; }

        [MaxLength(DataValidation.EquipmentAccessoariesLength)]
        public string Accessoaries { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Creation> Creations { get; set; }
    }
}