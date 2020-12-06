namespace SightsView.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "SightsView";

        public const string AdministratorRoleName = "Administrator";

        // Fixxed string messages
        public const string NoCountryOption = "Chose country";
        public const string NoCategoryOption = "Chose category for your creation";

        // Validations
        public const string CategoryNameLengthError = "Name length must be between 3 and 50 characters.";
        public const string CategoryDescriptionLengthError = "Name length must be between 3 and 500 characters.";

        public const string CommentContentLengthError = "Comment length must be between 1 and 255 characters.";

        public const string CreationTitleLengthError = "Creation title length must be between 2 and 150 characters.";
        public const string CreationDescriptionLengthError = "Creation description length must be between 3 and 150 characters.";

        public const string RepliesContentLengthError = "Reply must be between 1 and 255 characters.";

        public const string TagNameLengthError = "Tags name length must be between 1 and 40 characters.";

        // Custom Attributes constants
        public const string MaxFileSizeAttributeErrorr = "Maximum allowed file size is {0}MB.";

        public const string AllowedImageExtensionError = "Creation format is not supported.";
        public static readonly string[] AllowedImageExtensions = { ".jpg", ".png", ".jpeg", ".bmp" };
    }
}
