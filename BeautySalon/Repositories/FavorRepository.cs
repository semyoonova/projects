using BeautySalon.Abstractions;

namespace BeautySalon.Repositories
{
    public class FavorRepository : IFavorRepository
    {
        
        private List<Favor> _favors = new List<Favor>();
        private int _counter;
        public Task<int> Add(Favor favor)
        {
            favor.Id = _counter++;
            _favors.Add(favor);
            return Task.FromResult(favor.Id);
        }

        public Task<List<Favor>> GetAll()
        {
            return Task.FromResult(_favors);
        }

        public Task<Favor?> GetById(int id)
        {
            return Task.FromResult(_favors.FirstOrDefault(x => x.Id == id));
        }

        public Task<Favor> GetFavorByName(string name)
        {
            return Task.FromResult(_favors.First(x => x.FavorName == name));
        }

        public Task Remove(Favor favor)
        {
            _favors.Remove(favor);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            Console.WriteLine("изменения сохранены");
            return Task.CompletedTask;
        }
    }
}
