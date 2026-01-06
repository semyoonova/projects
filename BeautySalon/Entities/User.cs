

namespace BeautySalon
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public List<Register> Registers { get; set; } = new( );
        
    }
}
