namespace SightsView.Web.ViewModels.Equipments
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class EquipmentsEditInputModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public int? EquipmentId { get; set; }

        [StringLength(50, ErrorMessage = GlobalConstants.EquipmentBarndLengthError, MinimumLength = 2)]
        public string EquipmentBrand { get; set; }

        [StringLength(50, ErrorMessage = GlobalConstants.EquipmentModelLengthError, MinimumLength = 2)]
        public string EquipmentModel { get; set; }

        [StringLength(250, ErrorMessage = GlobalConstants.DetailsIsoLengthError, MinimumLength = 3)]
        public string EquipmentAccessoaries { get; set; }

        public string EquipmentNotes { get; set; }

        // Placeholder properties
        public string PlaceholderBrand
            => this.EquipmentBrand ?? string.Empty;

        public string PlaceholderModel
            => this.EquipmentModel ?? string.Empty;

        public string PlaceholderAccessoaries
            => this.EquipmentAccessoaries ?? "Accessoaries";

        public string PlaceholderNotes
            => this.EquipmentNotes ?? "Notes";
    }
}
