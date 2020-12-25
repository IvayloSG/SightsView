namespace SightsView.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using AutoMapper;

    using SightsView.Data.Models;
    using SightsView.Services.Mapping;
    using SightsView.Web.ViewModels.Replies;

    public class CommentsAllViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ApplicationUserId { get; set; }

        public string ApplicationUserUserName { get; set; }

        public ICollection<RepliesAllViewModel> Replies { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentsAllViewModel>()
                .ForMember(x => x.Replies, opt => opt.MapFrom(
                    x => x.Replies));
        }
    }
}
