using BeautySalon.Models;
using AutoMapper;

namespace BeautySalon.Abstractions
{
    public interface IFavorService
    {   
        Task<Favor> AddFavor(FavorDto favorDto);
        Task RemoveFavor(int favorId);
        Task<Favor> ChangeFavorPrice(int favorId, int newPrice);
        Task<Favor> FindFavorById(int favorId);
        Task<List<Favor>> GetAllFavors();
    }
}
