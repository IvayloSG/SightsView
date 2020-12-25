namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddCommentAsync(string content, string creationId, string userId);

        Task<string> DeleteCommentAsync(int commentId, string userId);

        Task<bool> EditCommentAsync(int commentId, string commentContent, string userId);

        Task<T> GetCommentsByIdAsync<T>(int commentId);

        Task<IEnumerable<T>> GetAllCommentsForCreationAsync<T>(string creationId);
    }
}
