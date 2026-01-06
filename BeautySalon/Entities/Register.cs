namespace BeautySalon
{
    public class Register
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MasterId {  get; set; }
        public int FavorId {  get; set; }
        public DateTime RegisterDate { get; set; }
        public Master Master { get; set; }
        public Favor Favor { get; set; }
        public User? User { get; set; } 
        public int Discount { get; set; }
    }
}
