using AspLessons.Contracts;
using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Configuration
{
    public class FavorMapperProfile : Profile
    {
        public FavorMapperProfile()
        {
            CreateMap<CreateFavorRequest, FavorDto>( )
                .ForMember(dest => dest.FavorName, opt => opt.MapFrom(src => src.FavorName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Id, opt => opt.Ignore( ));

            CreateMap<FavorDto, Favor>( )
                .ForMember(dest => dest.FavorName, opt => opt.MapFrom(src => src.FavorName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Id, opt => opt.Ignore( ));

            CreateMap<Favor, FavorDto>();

        }
    }
}
