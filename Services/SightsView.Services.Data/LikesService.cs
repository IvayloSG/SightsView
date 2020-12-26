namespace SightsView.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;

    public class LikesService : ILikesService
    {
        private readonly SightsView.Data.Common.Repositories.IDeletableEntityRepository<Like> likesRepository;

        public LikesService(IDeletableEntityRepository<Like> likesRepository)
        {
            this.likesRepository = likesRepository;
        }

        public async Task<int> GetLikesCountAsync(string creationId)
        {
            var likes = await this.likesRepository.AllAsNoTracking()
            .Where(x => x.CreationId == creationId).ToListAsync();

            return likes.Count();
        }

        public async Task LikeUnlikeAsync(string userId, string creationId)
        {
            var currentLike = await this.likesRepository.All()
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userId && x.CreationId == creationId);

            if (currentLike == null)
            {
                var like = new Like()
                {
                    ApplicationUserId = userId,
                    CreationId = creationId,
                };

                await this.likesRepository.AddAsync(like);
            }
            else
            {
                this.likesRepository.Delete(currentLike);
            }

            await this.likesRepository.SaveChangesAsync();
        }
    }
}
