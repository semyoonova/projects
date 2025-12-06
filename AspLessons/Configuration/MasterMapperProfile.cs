using AspLessons.Contracts;
using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Configuration
{
    public class MasterMapperProfile: Profile
    {
        public MasterMapperProfile()
        {
            CreateMap<CreateMasterRequest, MasterDto>( )
                .ForMember(dest => dest.Id , opt => opt.Ignore());
            CreateMap<Master, MasterDto>( )
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        }
    }
}
