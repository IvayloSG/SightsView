namespace SightsView.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;
    using SightsView.Services.Data.Contracts;

    public class FollowsService : IFollowsService
    {
        private readonly IDeletableEntityRepository<Follow> followsRepository;

        public FollowsService(IDeletableEntityRepository<Follow> followsRepository)
        {
            this.followsRepository = followsRepository;
        }

        public async Task FollowUnfollowAsync(string followerId, string followedId)
        {
            if (followerId == followedId)
            {
                return;
            }

            var currentFollow = await this.followsRepository.All()
                .FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowedId == followedId);

            if (currentFollow == null)
            {
                var follow = new Follow()
                {
                    FollowerId = followerId,
                    FollowedId = followedId,
                };

                await this.followsRepository.AddAsync(follow);
            }
            else
            {
                this.followsRepository.Delete(currentFollow);
            }

            await this.followsRepository.SaveChangesAsync();
        }

        public async Task<int> GetFollowersCountAsync(string userId)
        {
            var followers = await this.followsRepository.AllAsNoTracking()
                .Where(x => x.FollowedId == userId)
                .ToListAsync();

            return followers.Count();
        }
    }
}
