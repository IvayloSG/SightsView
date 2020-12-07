namespace SightsView.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;
    using SightsView.Services.Mapping;

    public class RepliesService : IRepliesService
    {
        private readonly IDeletableEntityRepository<Reply> repliesRepository;

        public RepliesService(IDeletableEntityRepository<Reply> repliesRepository)
        {
            this.repliesRepository = repliesRepository;
        }

        public async Task AddReplyToCommentAsync(string replyContent, int commentId, string userId)
        {
            var reply = new Reply()
            {
                Content = replyContent,
                CommentId = commentId,
                ApplicationUserId = userId,
            };

            await this.repliesRepository.AddAsync(reply);
            await this.repliesRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteReplyAsync(int replyId, string userId)
        {
            var reply = await this.repliesRepository.AllAsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == replyId);

            if (reply == null || reply.ApplicationUserId != userId)
            {
                return false;
            }

            this.repliesRepository.Delete(reply);
            await this.repliesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditReplyAsync(int replyId, string content, string userId)
        {
            var comment = await this.repliesRepository.All()
     .FirstOrDefaultAsync(x => x.Id == replyId);

            if (comment == null || comment.ApplicationUserId != userId)
            {
                return false;
            }

            comment.Content = content;

            this.repliesRepository.Update(comment);
            await this.repliesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<T> GetReplyByIdAsync<T>(int replyId)
            => await this.repliesRepository.All()
            .Where(x => x.Id == replyId)
            .To<T>()
            .FirstOrDefaultAsync();
    }
}
