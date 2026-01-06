namespace BeautySalon.Models
{
    public class RegisterDto
    {
        public int UserId { get; set; }
        public int MasterId { get; set; }
        public int FavorId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
