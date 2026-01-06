using BeautySalon.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Repositories
{
    public class MasterRepositoryEF : IMasterRepository
    {
        private ApplicationContext _aplicationContext;
        public MasterRepositoryEF(ApplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
        public async Task<int> Add(Master entity)
        {
            var result = await _aplicationContext.Masters.AddAsync(entity);
            return result.Entity.Id;
        }

        public async Task<List<Master>> GetAll()
        {
            return await _aplicationContext.Masters.ToListAsync( );
        }

        public async Task<Master?> GetById(int id)
        {
            return await _aplicationContext.Masters
                .Include(x => x.Favors)
                .Include(x => x.WorkHours)
                .Include(x => x.Registers)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Remove(Master entity)
        {
            _aplicationContext.Masters.Remove(entity);

        }
        public async Task SaveChangesAsync()
        {
            await _aplicationContext.SaveChangesAsync();
        }
        public async Task<Master?> GetMasterByName(string name)
        {
            return await _aplicationContext.Masters.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
