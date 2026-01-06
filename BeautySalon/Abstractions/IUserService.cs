using BeautySalon.Models;
using AutoMapper;

namespace BeautySalon.Abstractions
{
    public interface IUserService
    {
        Task<Register> RegistrationToFavor(RegisterDto register);
        Task<List<DateOnly>> GetWorkingDays(int masterId);
        Task<List<TimeOnly>> GetFreeSlots(RegisterDto register);
        Task<List<MasterDto>> GetAllMasters();
        Task<List<Favor>> GetAllFavors();
        Task<List<Favor>> GetAllFavorsByMaster(int masterId);
        Task<List<Favor>> GetNotAddedFavorsByMaster(int masterId);
        Task CreateUser(UserDto user);
        Task<AuthResult> LoginUser(string phoneNumber, string password);

    }
}
