using AspLessons.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AspLessons.Repositories
{
    public class WorkHoursRepositoryEF : IWorkHoursRepository
    {
        private ApplicationContext _aplicationContext;
        public WorkHoursRepositoryEF(ApplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
        public async Task<int> Add(WorkHours entity)
        {
            var result = await _aplicationContext.WorkHours.AddAsync(entity);
            return result.Entity.Id;
        }

        public async Task<List<WorkHours>> GetAll()
        {
            return await _aplicationContext.WorkHours.ToListAsync( );
        }

        public async Task<WorkHours?> GetById(int id)
        {
            return await _aplicationContext.WorkHours
                .Include(x => x.Masters)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WorkHours?> GetWorkHoursByDate(DateOnly date)
        {
            return await _aplicationContext.WorkHours.FirstOrDefaultAsync(x => x.Date == date);
        }

        public async Task Remove(WorkHours entity)
        {
            _aplicationContext.WorkHours.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _aplicationContext.SaveChangesAsync( );
        }
    }
}
