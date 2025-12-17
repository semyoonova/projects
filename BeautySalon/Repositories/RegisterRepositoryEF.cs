using AspLessons.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace AspLessons.Repositories
{
    public class RegisterRepositoryEF : IRegisterRepository
    {
        private ApplicationContext _aplicationContext;
        public RegisterRepositoryEF(ApplicationContext aplicationContext)
        {
            _aplicationContext = aplicationContext;
        }
        public async Task<int> Add(Register entity)
        {
            var result = await _aplicationContext.Registers.AddAsync(entity);
            return result.Entity.Id;
        }

        public async Task<List<Register>> GetAll()
        {
            return await _aplicationContext.Registers.ToListAsync( ); 
        }

        public async Task<Register?> GetById(int id)
        {
            return await _aplicationContext.Registers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Remove(Register entity)
        {
            _aplicationContext.Registers.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _aplicationContext.SaveChangesAsync();
        }

        public async Task<List<Register>> GetRegistersByDay(DateOnly date)
        {

            return await _aplicationContext.Registers.Where(register => DateOnly.FromDateTime(register.RegisterDate.Date) == date)
                .ToListAsync( );
        }
    }
}
