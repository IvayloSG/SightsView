namespace SightsView.Web.ViewModels.Creations
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class CreationsViewModel : IMapFrom<Creation>
    {
        public string Id { get; set; }

        public string CreationDataUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public int LikesCount { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }
    }
}
