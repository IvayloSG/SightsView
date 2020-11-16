namespace SightsView.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            this.maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > this.maxFileSize)
                {
                    return new ValidationResult(string.Format(GlobalConstants.MaxFileSizeAttributeErrorr, this.maxFileSize / 1024 / 1024));
                }
            }

            return ValidationResult.Success;
        }
    }
}
