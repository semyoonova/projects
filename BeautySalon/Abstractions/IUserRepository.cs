namespace BeautySalon.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByPhone(string phoneNumber);
    }
}
