namespace BeautySalon.Contracts
{
    public class RegisterResponse
    {
        public string MasterName { get; set; }
        public string FavorName { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

    }
}
