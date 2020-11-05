namespace SightsView.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Follow
    {
        [Required]
        public string FollowedId { get; set; }

        public ApplicationUser Followed { get; set; }

        [Required]
        public string FollowerId { get; set; }

        public ApplicationUser Follower { get; set; }
    }
}
