using BeautySalon.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Repositories
{
    public class FavorRepositoryEF : IFavorRepository
    {
        private ApplicationContext _aplicationContext;
        public FavorRepositoryEF(ApplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }

        public async Task<int> Add(Favor entity) 
        {
            var result = await _aplicationContext.Favors.AddAsync(entity);
            return result.Entity.Id;
        }

        public async  Task<List<Favor>> GetAll()
        {
            return await _aplicationContext.Favors.ToListAsync();  
        }

        public async Task<Favor?> GetById(int id)
        {
            return await _aplicationContext.Favors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Favor?> GetFavorByName(string name)
        {
            return await _aplicationContext.Favors.FirstOrDefaultAsync(x => x.FavorName == name);
        }

        public async Task Remove(Favor entity)
        {
            var result = _aplicationContext.Favors.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _aplicationContext.SaveChangesAsync();
        }
    }
}
