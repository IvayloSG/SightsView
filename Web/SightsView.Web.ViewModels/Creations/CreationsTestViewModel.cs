namespace SightsView.Web.ViewModels.Creations
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsTestViewModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreatorId { get; set; }

        public string StorageAddress { get; set; }

        public string CreationDataUrl { get; set; }
    }
}
