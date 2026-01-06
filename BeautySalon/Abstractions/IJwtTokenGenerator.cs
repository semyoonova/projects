using BeautySalon.Models;

namespace BeautySalon.Abstractions
{
    public interface IJwtTokenGenerator
    {
       
        string? CreateJwtToken(User user);
        
    }
}
