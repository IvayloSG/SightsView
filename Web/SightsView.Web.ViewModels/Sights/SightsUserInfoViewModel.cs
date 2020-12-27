namespace SightsView.Web.ViewModels.Sights
{
    using SightsView.Data.Models;
    using SightsView.Services.Mapping;

    public class SightsUserInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int CreationsCount { get; set; }

        public int FollowersCount { get; set; }
    }
}
