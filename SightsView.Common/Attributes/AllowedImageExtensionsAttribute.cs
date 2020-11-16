namespace SightsView.Common.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public class AllowedImageExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] allowedImageExtensions = GlobalConstants.AllowedImageExtensions;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!this.allowedImageExtensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GlobalConstants.AllowedImageExtensionError);
                }
            }

            return ValidationResult.Success;
        }
    }
}
