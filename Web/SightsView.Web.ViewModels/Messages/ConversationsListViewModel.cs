namespace SightsView.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class ConversationsListViewModel
    {
        public IEnumerable<MessagesUsernamesViewModel> Conversations { get; set; }
    }
}
