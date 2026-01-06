using BeautySalon.Models;

namespace BeautySalon.Abstractions
{
    public interface IRegisterService
    {
        Task<Register> CreateRegister(RegisterDto registerDto);
    }
}
