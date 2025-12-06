using AspLessons.Contracts;
using AspLessons.Models;
using AutoMapper;

namespace AspLessons.Configuration
{
    public class WorkHoursMapperProfile : Profile
    {
        public WorkHoursMapperProfile()
        {
            CreateMap<WorkHoursDto, CreateWorkHoursRequest>( )
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Begin, opt => opt.MapFrom(src => src.Begin))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

            CreateMap<WorkHours, WorkHoursDto>( )
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Begin, opt => opt.MapFrom(src => src.Begin))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

            CreateMap<WorkHoursDto, WorkHours>( )
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Begin, opt => opt.MapFrom(src => src.Begin))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Masters, opt => opt.Ignore());

            CreateMap<CreateWorkHoursRequest, WorkHoursDto>( )
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Begin, opt => opt.MapFrom(src => src.Begin))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

        }
    }
}
