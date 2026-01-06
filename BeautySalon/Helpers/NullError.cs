namespace BeautySalon.Helpers
{
    public class NullError
    {
        public string Error { get; set; }
        public string Message { get; set; }

    }

    public class NullException : Exception
    {
        public int StatusCode { get; }

        public NullException(string message, int statusCode = 400) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
