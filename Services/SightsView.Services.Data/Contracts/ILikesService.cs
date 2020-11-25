namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task LikeUnlikeAsync(string userId, string creationId);

        Task<int> GetLikesCountAsync(string userId, string creationId);
    }
}
