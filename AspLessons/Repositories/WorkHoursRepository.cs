using AspLessons.Abstractions;

namespace AspLessons.Repositories
{
    public class WorkHoursRepository : IWorkHoursRepository
    {
        private List<WorkHours> _allWorkHours = new List<WorkHours>( );
        private int _counter;
        public Task<int> Add(WorkHours entity)
        {
            entity.Id = _counter++;
            _allWorkHours.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task<List<WorkHours>> GetAll()
        {
            return Task.FromResult( _allWorkHours);
        }

        public Task<WorkHours> GetById(int id)
        {
            return Task.FromResult(_allWorkHours.FirstOrDefault(x => x.Id == id));
        }

        public Task<WorkHours> GetWorkHoursByDate(DateOnly date)
        {
            return Task.FromResult( _allWorkHours.FirstOrDefault(x => x.Date == date));
        }

        public Task Remove(WorkHours entity)
        {
            _allWorkHours.Remove(entity);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            Console.WriteLine("изменения сохранены");
            return Task.CompletedTask;
        }
    }
}
