namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Messages;
    using Xunit;

    public class MessagesServiceTest : BaseServiceTests
    {
        private IMessagesService Service => this.ServiceProvider.GetRequiredService<IMessagesService>();

        [Fact]
        public async Task DeleteMessageSuccessResultTest()
        {
            var message = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContent",
            };

            await this.DbContext.Messages.AddAsync(message);
            await this.DbContext.SaveChangesAsync();

            var userId = "13";

            var result = await this.Service.DeleteMessageAsync(message.Id, userId);
            var resultCount = await this.DbContext.Messages.CountAsync();

            var expectedCount = 0;

            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(message.ReceiverId, result);
        }

        [Fact]
        public async Task DeleteMessageNullResultTest()
        {
            var message = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContent",
            };

            await this.DbContext.Messages.AddAsync(message);
            await this.DbContext.SaveChangesAsync();

            var userId = "15";

            var result = await this.Service.DeleteMessageAsync(message.Id, userId);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteMessageThrowsExceptionTest()
        {
            var messageId = 15;
            var userId = "13";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
              () => this.Service.DeleteMessageAsync(messageId, userId));
        }

        [Fact]
        public async Task GetConversationResulttest()
        {
            var message = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContent",
            };

            var messageTwo = new Message()
            {
                SenderId = "13",
                ReceiverId = "16",
                Content = "TestContentTwo",
            };

            var messageThree = new Message()
            {
                SenderId = "7",
                ReceiverId = "13",
                Content = "TestContentThree",
            };

            var messageFour = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContentFour",
            };

            var messageFive = new Message()
            {
                SenderId = "15",
                ReceiverId = "7",
                Content = "TestContentFour",
            };

            await this.DbContext.Messages.AddAsync(message);
            await this.DbContext.Messages.AddAsync(messageTwo);
            await this.DbContext.Messages.AddAsync(messageThree);
            await this.DbContext.Messages.AddAsync(messageFour);
            await this.DbContext.Messages.AddAsync(messageFive);
            await this.DbContext.SaveChangesAsync();

            var currentUserId = "43";

            var result = await this.Service.GetConversationsAsync(currentUserId);
            var resultFirstElement = await this.DbContext.Messages.FirstOrDefaultAsync();

            var expectedCount = 0;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(message.SenderId, resultFirstElement.SenderId);
        }

        [Fact]
        public async Task GetMessagesOfConversationResulttest()
        {
            var message = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContent",
            };

            var messageTwo = new Message()
            {
                SenderId = "13",
                ReceiverId = "16",
                Content = "TestContentTwo",
            };

            var messageThree = new Message()
            {
                SenderId = "7",
                ReceiverId = "13",
                Content = "TestContentThree",
            };

            var messageFour = new Message()
            {
                SenderId = "13",
                ReceiverId = "7",
                Content = "TestContentFour",
            };

            var messageFive = new Message()
            {
                SenderId = "15",
                ReceiverId = "7",
                Content = "TestContentFour",
            };

            await this.DbContext.Messages.AddAsync(message);
            await this.DbContext.Messages.AddAsync(messageTwo);
            await this.DbContext.Messages.AddAsync(messageThree);
            await this.DbContext.Messages.AddAsync(messageFour);
            await this.DbContext.Messages.AddAsync(messageFive);
            await this.DbContext.SaveChangesAsync();

            var currentUserId = "7";
            var peerId = "13";

            var result = await this.Service.GetMessagesOfConversationAsync<MessagesTestViewModel>(currentUserId, peerId);
            var resultFirstElement = result.ToList()[0];

            var expectedCount = 3;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(messageFour.Id, resultFirstElement.Id);
            Assert.Equal(messageFour.Content, resultFirstElement.Content);
        }

        [Fact]
        public async Task SendMessageResultTest()
        {
            var senderId = "7";
            var receiverId = "13";
            var content = "TestContent";

            await this.Service.SendMessageAsync(senderId, receiverId, content);

            var result = await this.DbContext.Messages.FirstOrDefaultAsync();

            Assert.Equal(senderId, result.SenderId);
            Assert.Equal(receiverId, result.ReceiverId);
            Assert.Equal(content, result.Content);
        }
    }
}
