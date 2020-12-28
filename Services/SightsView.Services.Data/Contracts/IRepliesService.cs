namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IRepliesService
    {
        Task AddReplyToCommentAsync(string replyContent, int commentId, string userId);

        Task<bool> DeleteReplyAsync(int replyId, string userID);

        Task<T> GetReplyByIdAsync<T>(int replyId);

        Task<int> EditReplyAsync(int replyId, string content, string userId);
    }
}
