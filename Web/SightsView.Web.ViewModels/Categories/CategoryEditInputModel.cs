namespace SightsView.Web.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class CategoryEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = GlobalConstants.CategoryNameLengthValidation, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = GlobalConstants.CategoryDescriptionLengthValidation, MinimumLength = 3)]
        public string Description { get; set; }
    }
}
