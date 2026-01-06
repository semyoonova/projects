using BeautySalon.Models;
using AutoMapper;

namespace BeautySalon.Abstractions
{
    public interface IWorkHoursService
    {
        Task<WorkHours> AddWorkHours(WorkHoursDto workhoursDto);
        Task<WorkHours?> FindWorkHoursById(int workhoursId);
    }
}
