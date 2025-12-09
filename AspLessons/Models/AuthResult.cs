namespace AspLessons.Models
{
    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int Code { get; set; }
        public string Token { get; set; }
    }
}
