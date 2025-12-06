using AspLessons.Abstractions;

namespace AspLessons.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private List<Master> _masters = new List<Master>( );
        private int _counter;
        public Task<int> Add(Master entity)
        {
            entity.Id = _counter++;
            _masters.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<List<Master>> GetAll()
        {
            return Task.FromResult(_masters);
        }

        public Task<Master?> GetById(int id)
        {
            return Task.FromResult(_masters.FirstOrDefault(x => x.Id == id));
        }

        public Task Remove(Master entity)
        {
            _masters.Remove(entity);
            return Task.CompletedTask;
        }
        public Task SaveChangesAsync()
        {
            Console.WriteLine("изменения сохранены");
            return Task.CompletedTask;
        }
        public Task<Master?> GetMasterByName(string name)
        {
            return Task.FromResult(_masters.FirstOrDefault(x => x.Name == name));
        }
    }
}
