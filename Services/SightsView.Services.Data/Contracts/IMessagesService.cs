namespace SightsView.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content);
    }
}
