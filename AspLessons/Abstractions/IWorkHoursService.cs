using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Abstractions
{
    public interface IWorkHoursService
    {
        Task<WorkHours> AddWorkHours(WorkHoursDto workhoursDto);
        Task<WorkHours?> FindWorkHoursById(int workhoursId);
    }
}
