namespace AspLessons.Abstractions
{
    public interface IWorkHoursRepository : IRepository<WorkHours>
    {
        
        Task<WorkHours?> GetWorkHoursByDate(DateOnly date);

    }
}
