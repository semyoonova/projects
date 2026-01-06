

namespace BeautySalon
{
    public class WorkHours
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Begin { get; set; }
        public TimeOnly End { get; set; }
        public List<Master> Masters { get; set; } = new();
    }
}
