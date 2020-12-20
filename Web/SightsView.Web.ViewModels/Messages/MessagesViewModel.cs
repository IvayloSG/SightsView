namespace SightsView.Web.ViewModels.Messages
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class MessagesViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverUserName { get; set; }

        public string Content { get; set; }
    }
}
