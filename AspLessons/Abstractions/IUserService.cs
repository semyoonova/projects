using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Abstractions
{
    public interface IUserService
    {
        Task<Register> RegistrationToFavor(RegisterDto register);
        Task<List<DateOnly>> GetWorkingDays(int masterId);
        Task<List<TimeOnly>> GetFreeSlots(RegisterDto register);
        Task<List<MasterDto>> GetAllMasters();
        Task<List<Favor>> GetAllFavorsByMaster(int masterId);
        Task CreateUser(UserDto user);
        Task<AuthResult> LoginUser(string phoneNumber, string password);

    }
}
