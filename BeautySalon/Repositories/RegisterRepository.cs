using BeautySalon.Abstractions;
using System.Diagnostics.Metrics;

namespace BeautySalon.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private List<Register> _registers = new List<Register>( );
        private int _counter;
        public Task<int> Add(Register entity)
        {
            entity.Id = _counter++;
            _registers.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<List<Register>> GetAll()
        {
            return Task.FromResult(_registers);
        }

        public Task<Register> GetById(int id)
        {
            return Task.FromResult(_registers.First(x => x.Id == id));
        }

        public Task Remove(Register entity)
        {
            _registers.Remove(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            Console.WriteLine("изменения сохранены");
            return Task.CompletedTask;
        }

        public Task<List<Register>> GetRegistersByDay(DateOnly date)
        {

             return Task.FromResult(_registers.Where(register => DateOnly.FromDateTime(register.RegisterDate.Date) == date).ToList());
        }
    }
}
