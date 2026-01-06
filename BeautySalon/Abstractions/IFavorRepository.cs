namespace BeautySalon.Abstractions
{
    public interface IFavorRepository : IRepository<Favor>
    {
        Task<Favor> GetFavorByName (string name);
    }
}
