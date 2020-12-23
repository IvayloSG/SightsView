namespace SightsView.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "SightsView";

        public const string AdministratorRoleName = "Administrator";

        public const int CreationsPerPage = 10;

        // Fixxed string messages
        public const string NoCountryOption = "Chose country";
        public const string NoCategoryOption = "Chose category for your creation";

        public const string ConfirmationMailSubject = "Please confirm your email";
        public const string MailFrom = "sightsview@gmail.com";
        public const string MailFromName = "SightsView.Common";

        // Validations
        public const string UserNameLengthError = "Username length must be between 2 and 100 characters.";
        public const string UserNamesLengthError = "Name length must be between 2 and 50 characters.";
        public const string UserPasswordLengthError = "Password must be atleast 6 characters.";
        public const string UserPasswordAndConfirmPasswordMismatch = "Password and Connfirm Passwort do not match.";

        public const string CategoryNameLengthError = "Name length must be between 3 and 50 characters.";
        public const string CategoryDescriptionLengthError = "Name length must be between 3 and 500 characters.";

        public const string CommentContentLengthError = "Comment length must be between 1 and 255 characters.";

        public const string CreationTitleLengthError = "Creation title length must be between 2 and 150 characters.";
        public const string CreationDescriptionLengthError = "Creation description length must be between 3 and 150 characters.";

        public const string DetailsAperetureLengthError = "Apereture length must be between 1 and 20 characters.";
        public const string DetailsShutterSteedLengthError = "Shutter Speed length must be between 1 and 20 characters.";
        public const string DetailsIsoLengthError = "ISO length must be between 1 and 20 characters.";

        public const string EquipmentBarndLengthError = "Brand length must be between 2 and 50 characters.";
        public const string EquipmentModelLengthError = "Model length must be between 2 and 50 characters.";
        public const string EquipmentAccessoariesLengthError = "Accessoaries length must maximum 250 characters.";

        public const string MessagesContentLengthError = "Message length must be between 2 and 500 characters.";

        public const string RepliesContentLengthError = "Reply must be between 1 and 255 characters.";

        public const string TagNameLengthError = "Tags name length must be between 1 and 40 characters.";

        // Custom Attributes constants
        public const string MaxFileSizeAttributeErrorr = "Maximum allowed file size is {0}MB.";

        public const string AllowedImageExtensionError = "Creation format is not supported.";
        public static readonly string[] AllowedImageExtensions = { ".jpg", ".png", ".jpeg", ".bmp" };
    }
}
