namespace SightsView.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using SightsView.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content);

        Task<IEnumerable<MessagesUsernamesViewModel>> GetConversationsAsync(string currentUserId);

        Task<IEnumerable<T>> GetMessagesOfConversationAsync<T>(string currentUserId, string peerId);

        Task<string> DeleteMessageAsync(int messageId);
    }
}
