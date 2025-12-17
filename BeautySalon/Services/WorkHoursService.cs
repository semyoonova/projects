using AspLessons.Abstractions;
using AspLessons.Models;
using AspLessons.Repositories;
using AutoMapper;
using Microsoft.Win32;

namespace AspLessons.Services
{
    public class WorkHoursService(IWorkHoursRepository workHoursRepository, IMapper mapper) : IWorkHoursService
    {
        public async Task<WorkHours> AddWorkHours(WorkHoursDto workhoursDto)
        {
            int id = await workHoursRepository.GetWorkHoursIdByDateAndTime(workhoursDto);

            if(id == 0)
            {
                if(workhoursDto.Begin > workhoursDto.End)
                    throw new Exception("Некорректные данные");
                WorkHours workHours = mapper.Map<WorkHours>(workhoursDto);
                workHoursRepository.Add(workHours);
                await workHoursRepository.SaveChangesAsync( );
                return workHours;
            }
            return await FindWorkHoursById(id);
        }

        public async Task<WorkHours?> FindWorkHoursById(int workhoursId)
        {
            WorkHours workHours =await  workHoursRepository.GetById(workhoursId);
            return workHours;
        }


    }
}
