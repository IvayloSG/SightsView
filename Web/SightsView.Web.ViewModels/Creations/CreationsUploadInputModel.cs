namespace SightsView.Web.ViewModels.Creations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using SightsView.Common;
    using SightsView.Web.ViewModels.Tags;

    public class CreationsUploadInputModel
    {
        [Required]
        [StringLength(150, ErrorMessage = GlobalConstants.CreationTitleLengthError, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = GlobalConstants.CreationDescriptionLengthError, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public string Privacy { get; set; }

        public string Country { get; set; }

        public string Category { get; set; }

        [Required]
        public IFormFile Creation { get; set; }

        public IEnumerable<TagsInputModel> Tags { get; set; }
    }
}
