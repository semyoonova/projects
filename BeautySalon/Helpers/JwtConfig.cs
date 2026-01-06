namespace BeautySalon.Helpers
{
    public class JwtConfig
    {
        public int ExpiredAtMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string IssuerSignKey { get; set; }
    }
}
