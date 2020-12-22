namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Messaging;
    using SightsView.Web.ViewModels.Messages;
    using SightsView.Web.ViewModels.Photographers;

    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly IPhotographersService photographersService;
        private readonly IEmailSender emailSender;

        public MessagesController(IMessagesService messagesService, IPhotographersService photographersService, IEmailSender emailSender)
        {
            this.messagesService = messagesService;
            this.photographersService = photographersService;
            this.emailSender = emailSender;
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var conversations = await this.messagesService.GetConversationsAsync(userId);
            var viewModel = new ConversationsListViewModel()
            {
                Conversations = conversations,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var peerId = await this.messagesService.DeleteMessageAsync(id);

            return this.RedirectToAction("Load", new { id = peerId });
        }

        public async Task<IActionResult> Load(string id)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var messages = await this.messagesService.GetMessagesOfConversationAsync<MessagesViewModel>(currentUserId, id);

            var viewModel = new ConversationsLoadViewModel()
            {
                Messages = messages,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Send(string id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(MessagesSendInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (input.Id == senderId)
            {
                return this.BadRequest();
            }

            await this.messagesService.SendMessageAsync(senderId, input.Id, input.Content);

            var senderName = this.User.FindFirstValue(ClaimTypes.Name);
            var receiver = await this.photographersService.GetPhotographerByIdAsync<PhotographersEmailViewModel>(input.Id);

            // TODO: Make a constant
            var sb = new StringBuilder();
            sb.AppendLine($"<p1>Dear {receiver.UserName},</p1>");
            sb.AppendLine("<br />");
            sb.AppendLine($"<p1>You have recieved a new message from {senderName}:<p1>");
            sb.AppendLine("<br />");
            sb.AppendLine($"<p1>{input.Content}</p1>");

            var emailSubject = $"New message from {senderName}";

            await this.emailSender.SendEmailAsync(GlobalConstants.MailFrom, GlobalConstants.MailFromName, receiver.Email, emailSubject, sb.ToString());
            return this.View();
        }
    }
}
