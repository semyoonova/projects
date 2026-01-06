using BeautySalon.Models;

namespace BeautySalon.Abstractions
{
    public interface IWorkHoursRepository : IRepository<WorkHours>
    {
        
        Task<WorkHours?> GetWorkHoursByDate(DateOnly date);
        Task<int> GetWorkHoursIdByDateAndTime(WorkHoursDto workHours);
    }
}
