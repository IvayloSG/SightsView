namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Data.Common.Models;

    public class Follow : BaseDeletableModel<int>
    {
        [Required]
        public string FollowedId { get; set; }

        public ApplicationUser Followed { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public ApplicationUser Follower { get; set; }
    }
}
