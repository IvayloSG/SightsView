namespace SightsView.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class ConversationsLoadViewModel
    {
        public IEnumerable<MessagesViewModel> Messages { get; set; }
    }
}
