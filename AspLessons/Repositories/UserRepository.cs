using AspLessons.Abstractions;
using Microsoft.Win32;
using System.Diagnostics.Metrics;

namespace AspLessons.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users = new List<User>( );
        private int _counter;
        public Task<int> Add(User entity)
        {
            entity.Id = _counter++;
            _users.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<List<User>> GetAll()
        {
            return Task.FromResult(_users);
        }

        public Task<User?> GetById(int id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.Id == id));
        }

        public Task<User?> GetUserByPhone(string phoneNumber)
        {
            return Task.FromResult(_users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault());
        }

        public Task Remove(User entity)
        {
            _users.Remove(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            Console.WriteLine("изменения сохранены");
            return Task.CompletedTask;
        }
    }
}
