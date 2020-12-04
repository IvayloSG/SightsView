namespace SightsView.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SightsView.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        Task<IEnumerable<CommentsAllViewModel>> GetAllCommentsForCreation(string creationId);

        Task<bool> DeleteCommentAsync(int commentId, string userId);

        Task AddCommentAsync(string content, string creationId, string userId);
    }
}
