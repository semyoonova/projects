using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Abstractions
{
    public interface IFavorService
    {   
        Task<Favor> AddFavor(FavorDto favorDto);
        Task RemoveFavor(int favorId);
        Task<Favor> ChangeFavorPrice(int favorId, int newPrice);
        Task<Favor> FindFavorById(int favorId);
    }
}
