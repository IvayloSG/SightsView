namespace SightsView.Web.ViewModels.Equipments
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class EquipmentsAddInputModel
    {
        public string CreationId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstants.EquipmentBarndLengthError, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstants.EquipmentModelLengthError, MinimumLength = 2)]
        public string Model { get; set; }

        [StringLength(250, ErrorMessage = GlobalConstants.EquipmentAccessoariesLengthError, MinimumLength = 3)]
        public string Accessoaries { get; set; }

        public string Notes { get; set; }
    }
}
