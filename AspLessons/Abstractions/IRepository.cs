namespace AspLessons.Abstractions
{
    public interface IRepository<T>
    {
        Task<int> Add(T entity);
        Task<List<T>> GetAll();
        Task<T?> GetById(int id);
        Task Remove(T entity);
        Task SaveChangesAsync();
    }
}
