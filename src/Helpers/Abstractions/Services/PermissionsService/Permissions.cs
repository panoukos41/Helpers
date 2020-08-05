namespace Panoukos41.Helpers.Services
{
    /// <summary>
    /// Provides implemented permissions based on android permissions.
    /// In Uwp and Ios there is a switch to evaluate strings since
    /// there is no class that provides these values.
    /// using these values.
    /// </summary>
    public static class Permissions
    {
        /// <summary>
        /// </summary>
        public const string WriteCalendar = "android.permission.WRITE_CALENDAR";

        /// <summary>
        /// </summary>
        public const string ReadCalendar = "android.permission.READ_CALENDAR";

        /// <summary>
        /// </summary>
        public const string Camera = "android.permission.CAMERA";

        /// <summary>
        /// </summary>
        public const string WriteContacts = "android.permission.WRITE_CONTACTS";

        /// <summary>
        /// </summary>
        public const string ReadContacts = "android.permission.READ_CONTACTS";

        /// <summary>
        /// </summary>
        public const string AccessFineLocation = "android.permission.ACCESS_FINE_LOCATION";

        /// <summary>
        /// </summary>
        public const string RecordAudio = "android.permission.RECORD_AUDIO";

        /// <summary>
        /// </summary>
        public const string CallPhone = "android.permission.CALL_PHONE";

        /// <summary>
        /// </summary>
        public const string BodySensors = "android.permission.BODY_SENSORS";

        /// <summary>
        /// </summary>
        public const string WriteSms = "android.permission.WRITE_SMS";

        /// <summary>
        /// </summary>
        public const string ReadSms = "android.permission.READ_SMS";

        /// <summary>
        /// </summary>
        public const string WriteStorage = "android.permission.WRITE_EXTERNAL_STORAGE";

        /// <summary>
        /// </summary>
        public const string ReadStorage = "android.permission.READ_EXTERNAL_STORAGE";
    }
}