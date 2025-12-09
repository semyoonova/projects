namespace AspLessons.Abstractions
{
    public interface IRegisterRepository : IRepository<Register>
    {
        public Task<List<Register>> GetRegistersByDay (DateOnly date);
    }
}
