namespace SightsView.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Messages;

    public class MessagesController : Controller
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public IActionResult Index()
        {
            return this.View();
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

            return this.View();
        }
    }
}
