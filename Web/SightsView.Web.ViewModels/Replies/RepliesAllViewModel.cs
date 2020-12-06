namespace SightsView.Web.ViewModels.Replies
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class RepliesAllViewModel : IMapFrom<Reply>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int CommentId { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }
    }
}
