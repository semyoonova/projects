namespace BeautySalon
{
    public class Master
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public List<Favor> Favors { get; set; } = new( );
        public List<WorkHours> WorkHours { get; set; } = new( );
        public List<Register> Registers { get; set; } = new( );

    }
}
