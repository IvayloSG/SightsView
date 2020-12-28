namespace SightsView.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SightsView.Common;
    using SightsView.Services.Contracts;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Messaging;
    using SightsView.Web.ViewModels.Messages;
    using SightsView.Web.ViewModels.Photographers;

    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;
        private readonly IPhotographersService photographersService;
        private readonly IEmailSender emailSender;
        private readonly IStringHelpersService stringProcessor;

        public MessagesController(
            IMessagesService messagesService,
            IPhotographersService photographersService,
            IEmailSender emailSender,
            IStringHelpersService stringProcessor)
        {
            this.messagesService = messagesService;
            this.photographersService = photographersService;
            this.emailSender = emailSender;
            this.stringProcessor = stringProcessor;
        }

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var peerId = await this.messagesService.DeleteMessageAsync(id, currentUserId);
                if (peerId == null)
                {
                    return this.RedirectToAction(nameof(this.All));
                }

                return this.RedirectToAction("Load", new { id = peerId });
            }
            catch (NullReferenceException nre)
            {
                return this.BadRequest(nre.Message);
            }
        }

        [Authorize]
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
        [Authorize]
        public IActionResult Send(string id)
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Send(MessagesSendInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (input.Id == senderId)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.messagesService.SendMessageAsync(senderId, input.Id, input.Content);

            var senderUserName = this.User.FindFirstValue(ClaimTypes.Name);
            var receiver = await this.photographersService.GetPhotographerByIdAsync<PhotographersEmailViewModel>(input.Id);
            var emailSubject = string.Format(GlobalConstants.MessageEmailSubject, senderUserName);

            var messageContent = this.stringProcessor.GetEmailContent(receiver.UserName, senderUserName, input.Content);

            await this.emailSender.SendEmailAsync(GlobalConstants.MailFrom, GlobalConstants.MailFromName, receiver.Email, emailSubject, messageContent);

            return this.RedirectToAction("Load", new { id = input.Id });
        }
    }
}
