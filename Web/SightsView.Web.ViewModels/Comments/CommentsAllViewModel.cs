namespace SightsView.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using SightsView.Data.Models;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Replies;

    public class CommentsAllViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }

        public ICollection<RepliesAllViewModel> Replies { get; set; }
    }
}
