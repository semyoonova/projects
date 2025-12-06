using AspLessons.Models;

namespace AspLessons.Abstractions
{
    public interface IJwtTokenGenerator
    {
       
        string? CreateJwtToken(User user);
        
    }
}
