

namespace AspLessons
{
    public class Favor
    {
        public int Id { get; set; }
        public string FavorName {  get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public List<Master> Masters { get; set; } = new( );
        //public List<Register> Registers { get; set; } = new( );
    }
}

