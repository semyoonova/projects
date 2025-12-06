using AspLessons.Abstractions;
using AspLessons.Helpers;
using AspLessons.Models;
using AspLessons.Repositories;
using AutoMapper;

namespace AspLessons.Services
{
    public class FavorService(IFavorRepository favorRepository, IMapper mapper) : IFavorService
    {
        public async Task<Favor> AddFavor(FavorDto favorDto)
        {
            Favor favor = mapper.Map<Favor>(favorDto);
            if(favor.Price < 0)
            {
                throw new Exception("Цена меньше нуля");
            }
            if(favor.Duration < 0)
            {
                throw new Exception("Длительность меньше нуля");
            }
            if (favorRepository.GetFavorByName(favor.FavorName)!=null)
            {
                throw new Exception("Услуга с таким названием уже есть");
            }
            favorRepository.Add(favor);
            await favorRepository.SaveChangesAsync();
            return await Task.FromResult(favor);
        }

        public async Task<Favor> ChangeFavorPrice(int favorId, int newPrice)
        {
            if(newPrice < 0)
            {
                throw new Exception("Новая цена меньше нуля");
            }
            
            Favor favor = await favorRepository.GetById(favorId);
            EntityChecker.Check(favor);
            favor.Price = newPrice;
            await favorRepository.SaveChangesAsync( );
            return favor;
        }

        public async Task<Favor> FindFavorById(int favorId)
        {
            Favor? favor = await favorRepository.GetById(favorId);
            EntityChecker.Check(favor);
            return favor;
        }

        public async Task RemoveFavor(int favorId)
        {
            Favor favor =await  favorRepository.GetById(favorId);
            EntityChecker.Check(favor);
            favorRepository.Remove(favor);
            await favorRepository.SaveChangesAsync( );
        }


    }
}
