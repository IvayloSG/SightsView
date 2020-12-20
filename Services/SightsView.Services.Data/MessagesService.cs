namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Messages;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task<string> DeleteMessageAsync(int messageId)
        {
            var message = await this.messagesRepository.All()
                .FirstOrDefaultAsync(x => x.Id == messageId);

            if (message == null)
            {
                return null;
            }

            var result = message.ReceiverId;

            this.messagesRepository.Delete(message);
            await this.messagesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<MessagesUsernamesViewModel>> GetConversationsAsync(string currentUserId)
             => await this.messagesRepository.AllAsNoTracking()
                .Where(x => x.ReceiverId == currentUserId || x.SenderId == currentUserId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new MessagesUsernamesViewModel()
                {
                    PeerId = x.SenderId == currentUserId ? x.ReceiverId : x.SenderId,
                    PeerUsername = x.SenderId == currentUserId ? x.Receiver.UserName : x.Sender.UserName,
                })
                .Distinct()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetMessagesOfConversationAsync<T>(string currentUserId, string peerId)
            => await this.messagesRepository.All()
                .Where(x => (x.SenderId == currentUserId && x.ReceiverId == peerId) ||
                            (x.SenderId == peerId && x.ReceiverId == currentUserId))
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task SendMessageAsync(string senderId, string receiverId, string content)
        {
            var message = new Message()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }
    }
}
