namespace Shared.Helpers
{
    public static class Constants
    {
        public const string ConnectionStringName = "Default";

        public const string RowVersionColumnName = "RowVersion";

        /// <summary>
        /// Regular expression that matches only alphabetical string
        /// </summary>
        public const string ReguarlExpressionAlphabetical = @"^[\p{L}\s]*$";
        public const string ReguarlExpressionAlphabeticalWithApostrophe = @"^[\p{L}\s']*$";
        public const string RegularExpressionPhoneNumber = @"^\d{2,5}-\d{4,13}$";
        public const string ReguarlExpressionEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public const string ReguarlExpressionAlphanumeric = @"^[\p{L}\s0-9٠-٩]+$";
        public const string ReguarlExpressionEnglishAlphanumericWithoutSpaces = @"^[a-zA-Z0-9]*$";
        public const string ReguarlExpressionNonNumric = "^[^0-9]*$";
        public const string ReguarlExpressionNumric = "^[0-9]*$";
        public const string ReguarlExpressionNoSpaces = "^[^\\s]*$";
        public const string RegularExpressionLengthRange = "^.{{{0},{1}}}$"; //Escape curly braces are used for string.Format

        public const string DefaultLocalizationSourceName = "Pharmacy";
        public const string AutoCompletePageSize = "App.UserManagement.AutoCompletePageSize";

        //Resoruces keys
        public const string ThisFieldIsAlphabeticalWithApostropheResourceKey = "ThisFieldIsAlphabeticalWithApostrophe";


        /// <summary>
        /// Notification keys
        /// </summary>
        public const string SendNotificationInput = "SendNotificationInput";
        public const string ContractNotificationType = "ContractNotificationType";
        public const string BookingNotificationType = "BookingNotificationType";
        public const string SimahNotificationType = "SimahNotificationType";
        public const string DamagesNotificationType = "DamagesNotificationType";

        public static class TenantManagement
        {
            public const string EnableCache = "App.TenantManagement.EnableCache";
        }
        public static class SettingsDefaultValues
        {
            public const string EnableCache = "true";
        }

        public static class JobConstants
        {
            public const string SystemJobsExecutor = "SystemJobsExecutor";
            public const string SystemJobsExecutorEmail = "SystemJobsExecutor@mail.com";
            public const string TenantJobsExecutor = "TenantJobsExecutor";
            public const string TenantJobsExecutorEmail = "TenantJobsExecutor@mail.com";
            public const string TenantIdArgument = "TenantId";
        }

        public static class CountryIsoCodes
        {
            public const string SaudiArabia = "SAR";
        }

    }
}
