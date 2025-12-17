using AspLessons.Models;

namespace AspLessons.Abstractions
{
    public interface IWorkHoursRepository : IRepository<WorkHours>
    {
        
        Task<WorkHours?> GetWorkHoursByDate(DateOnly date);
        Task<int> GetWorkHoursIdByDateAndTime(WorkHoursDto workHours);
    }
}
