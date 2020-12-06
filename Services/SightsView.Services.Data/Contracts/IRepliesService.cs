﻿namespace SightsView.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using SightsView.Data.Common.Repositories;
    using SightsView.Data.Models;

    public interface IRepliesService
    {
        Task AddReplyToCommentAsync(string replyContent, int commentId, string userId);

        Task<bool> DeleteReplyAsync(int replyId, string userID);
    }
}