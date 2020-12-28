namespace SightsView.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SightsView.Common;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Web.ViewModels.Comments;
    using Xunit;

    public class CommentsServiceTests : BaseServiceTests
    {
        private ICommentsService Service => this.ServiceProvider.GetRequiredService<ICommentsService>();

        [Fact]
        public async Task AddCommentAsyncResultTest()
        {
            var content = "TestContent";
            var creationId = "7";
            var userId = "13";

            await this.Service.AddCommentAsync(content, creationId, userId);

            var expectedCount = 1;

            var result = await this.DbContext.Comments.ToListAsync();

            Assert.Equal(expectedCount, result.Count);
            Assert.Equal(content, result[0].Content);
            Assert.Equal(creationId, result[0].CreationId);
            Assert.Equal(userId, result[0].ApplicationUserId);
        }

        [Fact]
        public async Task DeleteCommentResultTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var userId = "13";
            var result = await this.Service.DeleteCommentAsync(comment.Id, userId);

            var resultCount = await this.DbContext.Comments.CountAsync();
            var expectedCount = 0;

            Assert.Equal(expectedCount, resultCount);
            Assert.Equal(result, comment.CreationId);
        }

        [Fact]
        public async Task DeleteCommentAsyncReturnsInvalidUserMessageTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var userId = "31";
            var result = await this.Service.DeleteCommentAsync(comment.Id, userId);

            Assert.Equal(ExceptionMessages.InvalidUser, result);
        }

        [Fact]
        public async Task DeleteCommentThrowsNullReferenceExceptionTest()
        {
            var id = 5;
            var userId = "13";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
             () => this.Service.DeleteCommentAsync(id, userId));
        }

        [Fact]
        public async Task EditCommentTrueResultTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var editedContent = "EditedTestComment";

            var result = await this.Service.EditCommentAsync(comment.Id, editedContent, comment.ApplicationUserId);
            var resultComment = await this.DbContext.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

            var expectedResult = true;

            Assert.Equal(expectedResult, result);
            Assert.Equal(editedContent, resultComment.Content);
        }

        [Fact]
        public async Task EditCommentFalseResultTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var editedContent = "EditedTestComment";

            var userId = "31";
            var result = await this.Service.EditCommentAsync(comment.Id, editedContent, userId);
            var resultComment = await this.DbContext.Comments.FirstOrDefaultAsync(x => x.Id == comment.Id);

            var expectedResult = false;

            Assert.Equal(expectedResult, result);
            Assert.Equal(comment.Content, resultComment.Content);
        }

        [Fact]
        public async Task EditCommentThrowsNullReferenceExceptionTest()
        {
            var id = 5;
            var userId = "13";
            var editedContend = "EditedTestContent";

            var ex = await Assert.ThrowsAsync<NullReferenceException>(
             () => this.Service.EditCommentAsync(id, editedContend, userId));
        }

        [Fact]
        public async Task GetCommentsByIdResultTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCommentsByIdAsync<CommentsEditViewModel>(comment.Id);

            Assert.Equal(comment.Content, result.Content);
            Assert.Equal(comment.CreationId, result.CreationId);
        }

        [Fact]
        public async Task GetAllCommentsForCreationReturnResult()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            var commentTwo = new Comment()
            {
                Content = "TestContentTwo",
                CreationId = "7",
                ApplicationUserId = "15",
            };

            var commentThree = new Comment()
            {
                Content = "TestContentThree",
                CreationId = "8",
                ApplicationUserId = "15",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.Comments.AddAsync(commentTwo);
            await this.DbContext.Comments.AddAsync(commentThree);
            await this.DbContext.SaveChangesAsync();

            var creationId = "7";

            var result = await this.Service.GetAllCommentsForCreationAsync<CommentsEditViewModel>(creationId);
            var resultList = result.ToList();
            var resultComment = resultList[0];

            var expectedCount = 2;

            Assert.Equal(expectedCount, result.Count());
            Assert.Equal(comment.Id, resultComment.Id);
            Assert.Equal(comment.Content, resultComment.Content);
        }

        [Fact]
        public async Task GetCreationIdByCommentIdResultTest()
        {
            var comment = new Comment()
            {
                Content = "TestContent",
                CreationId = "7",
                ApplicationUserId = "13",
            };

            await this.DbContext.Comments.AddAsync(comment);
            await this.DbContext.SaveChangesAsync();

            var result = await this.Service.GetCreationIdByCommentAsync(comment.Id);

            Assert.Equal(result, comment.CreationId);
        }
    }
}
