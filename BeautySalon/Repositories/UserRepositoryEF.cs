using AspLessons.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AspLessons.Repositories
{
    
    public class UserRepositoryEF : IUserRepository
    {

        private ApplicationContext _aplicationContext;
        public UserRepositoryEF(ApplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
        public async Task<int> Add(User entity)
        {
            var result = await _aplicationContext.Users.AddAsync(entity);
            return result.Entity.Id;

        }

        public async Task<List<User>> GetAll()
        {
            return await _aplicationContext.Users.ToListAsync( );
        }

        public async Task<User?> GetById(int id)
        {
            return await _aplicationContext.Users
                .Include(x => x.Registers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByPhone(string phoneNumber)
        {
            return await _aplicationContext.Users
                .Include(x => x.Registers)
                .Where(x => x.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
        }

        public async Task Remove(User entity)
        {
            _aplicationContext.Users.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _aplicationContext.SaveChangesAsync();
        }
    }
}
