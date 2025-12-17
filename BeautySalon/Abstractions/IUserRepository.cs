namespace AspLessons.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByPhone(string phoneNumber);
    }
}
