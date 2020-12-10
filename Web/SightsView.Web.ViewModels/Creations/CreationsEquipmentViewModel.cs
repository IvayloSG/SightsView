namespace SightsView.Web.ViewModels.Creations
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsEquipmentViewModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        public int? EquipmentId { get; set; }

        public string EquipmentBrand { get; set; }

        public string EquipmentModel { get; set; }

        public string EquipmentAccessoaries { get; set; }

        public string EquipmentNotes { get; set; }

        // Rendering Properties
        public string BrandView
            => this.EquipmentBrand ?? "N/A";

        public string ModelView
            => this.EquipmentModel ?? "N/A";

        public string AccessoariesView
           => this.EquipmentAccessoaries ?? "N/A";

        public string NotesView
           => this.EquipmentNotes ?? string.Empty;
    }
}
