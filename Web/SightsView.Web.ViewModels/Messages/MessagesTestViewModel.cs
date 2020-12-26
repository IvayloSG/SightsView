namespace SightsView.Web.ViewModels.Messages
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class MessagesTestViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
