namespace AspLessons.Abstractions
{
    public interface IMasterRepository :IRepository<Master>
    {
        Task<Master?> GetMasterByName(string name);
    }
}


