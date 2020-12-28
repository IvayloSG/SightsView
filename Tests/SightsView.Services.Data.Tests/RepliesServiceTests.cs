namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Replies;
    using Xunit;

    public class RepliesServiceTests : BaseServiceTests
    {
        private IRepliesService Service => this.ServiceProvider.GetRequiredService<IRepliesService>();

        [Fact]
        public async Task AddReplyToCommentResultTest()
        {
            var content = "TestReply";
            var commentId = 3;
            var userId = "7";

            await this.Service.AddReplyToCommentAsync(content, commentId, userId);

            var result = await this.DbContext.Replies.FirstOrDefaultAsync();

            Assert.Equal(content, result.Content);
            Assert.Equal(commentId, result.CommentId);
            Assert.Equal(userId, result.ApplicationUserId);
        }

        [Fact]
        public async Task DeleteReplyResultTest()
        {
            var reply = new Reply()
            {
                Content = "TestContent",
                CommentId = 1,
                ApplicationUserId = "13",
            };

            await this.DbContext.Replies.AddAsync(reply);
            await this.DbContext.SaveChangesAsync();

            var userId = "13";
            var result = await this.Service.DeleteReplyAsync(reply.Id, userId);

            var resultCount = await this.DbContext.Replies.CountAsync();
            var expectedCount = 0;

            Assert.Equal(expectedCount, resultCount);
        }

        [Fact]
        public async Task DeleteReplyAsyncReturnsInvalidUserMessageTest()
        {
            var reply = new Reply()
            {
                Content = "TestContent",
                CommentId = 1,
                ApplicationUserId = "13",
            };

            await this.DbContext.Replies.AddAsync(reply);
            await this.DbContext.SaveChangesAsync();

            var userId = "31";
            var result = await this.Service.DeleteReplyAsync(reply.Id, userId);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteReplyThrowsNullReferenceExceptionTest()
        {
            var id = 5;
            var userId = "13";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
             () => this.Service.DeleteReplyAsync(id, userId));
        }

        [Fact]
        public async Task EditReplyTrueResultTest()
        {
            var reply = new Reply()
            {
                Content = "TestContent",
                CommentId = 7,
                ApplicationUserId = "13",
            };

            await this.DbContext.Replies.AddAsync(reply);
            await this.DbContext.SaveChangesAsync();

            var editedContent = "EditedTestContent";

            var result = await this.Service.EditReplyAsync(reply.Id, editedContent, reply.ApplicationUserId);
            var resultComment = await this.DbContext.Replies.FirstOrDefaultAsync(x => x.Id == reply.Id);

            Assert.Equal(editedContent, resultComment.Content);
        }

        [Fact]
        public async Task EditReplyFalseResultTest()
        {
            var reply = new Reply()
            {
                Content = "TestContent",
                CommentId = 7,
                ApplicationUserId = "13",
            };

            await this.DbContext.Replies.AddAsync(reply);
            await this.DbContext.SaveChangesAsync();

            var editedContent = "EditedTestContent";

            var userId = "31";
            var result = await this.Service.EditReplyAsync(reply.Id, editedContent, userId);
            var resultComment = await this.DbContext.Replies.FirstOrDefaultAsync(x => x.Id == reply.Id);

            var expectedResult = 0;

            Assert.Equal(expectedResult, result);
            Assert.Equal(reply.Content, resultComment.Content);
        }

        [Fact]
        public async Task EditReplyThrowsNullReferenceExceptionTest()
        {
            var id = 5;
            var userId = "13";
            var editedContend = "EditedTestContent";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
             () => this.Service.EditReplyAsync(id, editedContend, userId));
        }

        [Fact]
        public async Task GetReplyByIdResultTest()
        {
            var reply = new Reply()
            {
                Content = "TestContent",
                CommentId = 7,
                ApplicationUserId = "13",
            };

            await this.DbContext.Replies.AddAsync(reply);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetReplyByIdAsync<RepliesEditViewModel>(reply.Id);

            Assert.Equal(reply.Content, result.Content);
            Assert.Equal(reply.Id, result.Id);
        }
    }
}
