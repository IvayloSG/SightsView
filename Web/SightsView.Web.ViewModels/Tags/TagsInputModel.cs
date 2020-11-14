namespace SightsView.Web.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;

    using SightsView.Common;

    public class TagsInputModel
    {
        [StringLength(40, ErrorMessage = GlobalConstants.TagNameLengthError, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
