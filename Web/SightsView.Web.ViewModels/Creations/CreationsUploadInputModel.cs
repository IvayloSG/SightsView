namespace SightsView.Web.ViewModels.Creations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using SightsView.Common;
    using SightsView.Common.Attributes;

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
        [AllowedImageExtensions]
        [MaxFileSize(8 * 1024 * 1024)]
        public IFormFile Creation { get; set; }

        public string Tags { get; set; }

        public IList<SelectListItem> Categories { get; set; }

        public IList<SelectListItem> Countries { get; set; }
    }
}
