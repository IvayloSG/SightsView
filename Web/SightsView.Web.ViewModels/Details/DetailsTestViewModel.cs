namespace SightsView.Web.ViewModels.Details
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class DetailsTestViewModel : IMapFrom<Details>
    {
        public int Id { get; set; }

        public string Apereture { get; set; }
    }
}
