namespace AspLessons.Helpers
{
    public static class LoggerExtentions
    {
        public static void LogWithMaskedPhone(this ILogger logger, LogLevel level, string message, string phoneNumber)
        {
            string maskedPhone = MaskPhone(phoneNumber);
            logger.Log(level, message.Replace("{phone}", maskedPhone));
        }

        private static string MaskPhone(string phone)
        {
            if(string.IsNullOrEmpty(phone) || phone.Length < 4)
                return phone;
            return new string('x', phone.Length - 4) + phone.Substring(phone.Length - 4);
        }
    }
}
