namespace SightsView.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task AddCommentAsync(string content, string creationId, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                CreationId = creationId,
                ApplicationUserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCommentAsync(int commentId, string userId)
        {
            var comment = await this.commentsRepository.AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == commentId);

            if (comment == null || userId != comment.ApplicationUserId)
            {
                return false;
            }

            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditCommentAsync(int commentId, string commentContent, string userId)
        {
            var comment = await this.commentsRepository.All()
                .FirstOrDefaultAsync(x => x.Id == commentId);

            if (comment == null || comment.ApplicationUserId != userId)
            {
                return false;
            }

            comment.Content = commentContent;

            this.commentsRepository.Update(comment);
            await this.commentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllCommentsForCreationAsync<T>(string creationId)
        {
            var comments = await this.commentsRepository.AllAsNoTracking()
                .Where(x => x.CreationId == creationId)
                .To<T>()
                .ToListAsync();

            return comments;
        }

        public async Task<T> GetCommentsByIdAsync<T>(int commentId)
            => await this.commentsRepository.All()
            .Where(x => x.Id == commentId)
            .To<T>()
            .FirstOrDefaultAsync();
    }
}
