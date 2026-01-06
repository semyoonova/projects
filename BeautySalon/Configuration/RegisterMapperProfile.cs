using BeautySalon.Contracts;
using BeautySalon.Models;
using AutoMapper;
using System.Diagnostics.Metrics;

namespace BeautySalon.Configuration
{
    public class RegisterMapperProfile : Profile 
    {
        public RegisterMapperProfile()
        {
            CreateMap<Register, RegisterResponse>()
                .ForMember(dest => dest.MasterName, opt => opt.MapFrom(src => src.Master.Name))
                .ForMember(dest => dest.FavorName, opt => opt.MapFrom(src => src.Favor.FavorName))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.RegisterDate)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => TimeOnly.FromDateTime(src.RegisterDate)));

            CreateMap<RegisterFromUserRequest, RegisterDto>();
            CreateMap<GetSlotsRequest, RegisterDto>()
                .ForMember(dest => dest.Time, opt => opt.Ignore());

            CreateMap<RegisterDto, Register>()
                .ForMember(dest => dest.MasterId, opt => opt.MapFrom(src => src.MasterId))
                .ForMember(dest => dest.FavorId, opt => opt.MapFrom(src => src.FavorId))
                //.ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src =>DateTime
                //    .SpecifyKind(new DateTime(src.Date.Year, src.Date.Month, src.Date.Day,
                //    src.Time.Hour, src.Time.Minute, src.Time.Second), DateTimeKind.Utc)))
                .ForMember(dest => dest.RegisterDate, opt => opt.MapFrom(src =>
                    new DateTime(src.Date, src.Time)))
                .ForMember(dest => dest.Master, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ))
                .ForMember(dest => dest.Favor, opt => opt.Ignore());

        }
    }
}
