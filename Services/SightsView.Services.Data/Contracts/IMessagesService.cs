namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SightsView.Web.ViewModels.Messages;

    public interface IMessagesService
    {
        Task<string> DeleteMessageAsync(int messageId, string currentUserId);

        Task<IEnumerable<MessagesUsernamesViewModel>> GetConversationsAsync(string currentUserId);

        Task<IEnumerable<T>> GetMessagesOfConversationAsync<T>(string currentUserId, string peerId);

        Task SendMessageAsync(string senderId, string receiverId, string content);
    }
}
