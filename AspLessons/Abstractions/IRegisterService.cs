using AspLessons.Models;

namespace AspLessons.Abstractions
{
    public interface IRegisterService
    {
        Task<Register> CreateRegister(RegisterDto registerDto);
    }
}
