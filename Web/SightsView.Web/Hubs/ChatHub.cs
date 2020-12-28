namespace SightsView.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using SightsView.Web.ViewModels.Chat;

    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            var messageModel = new ChatMessage
            {
                User = this.Context.User.Identity.Name,
                Text = message,
            };

            await this.Clients.All.SendAsync("NewMessage", messageModel);
        }
    }
}
