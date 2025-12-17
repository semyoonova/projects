using AspLessons.Models;
using AspLessons.Repositories;

namespace AspLessons.Abstractions
{
    public interface IMasterService
    {
        Task AddFavorToMaster(int favorId, int masterId);
        Task RemoveFavorFromMaster(int favorId, int masterId);
        Task AddWorkHoursToMaster(WorkHoursDto workHours, int masterId);
        Task RemoveWorkHoursFromMaster(int workHoursId, int masterId);
        Task<Master> FindMasterById(int masterId);
        Task<Master> AddMaster(string name);
        Task RemoveMaster(Master master);
        Task<List<MasterDto>> GetAllMasters();
        Task<List<Favor>> GetAllFavorsByMaster(int masterId);
        Task<List<Favor>> GetNotAddedFavorsByMaster(int masterId);


    }
}
