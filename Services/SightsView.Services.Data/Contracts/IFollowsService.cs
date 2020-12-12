namespace SightsView.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFollowsService
    {
        Task<int> GetFollowersCountAsync(string userId);

        Task FollowUnfollowAsync(string followerId, string followedId);
    }
}
