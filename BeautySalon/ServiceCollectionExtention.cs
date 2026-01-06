using BeautySalon.Abstractions;
using BeautySalon.Configuration;
using BeautySalon.Helpers;
using BeautySalon.Repositories;
using BeautySalon.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BeautySalon
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>( );
            services.AddScoped<IFavorService, FavorService>( );
            services.AddScoped<IMasterService, MasterService>( );
            services.AddScoped<IWorkHoursService, WorkHoursService>( );
            services.AddScoped<IRegisterService, RegisterService>( );
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>( );
            services.AddSingleton<GlobalExceptionMiddleware>( );
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IFavorRepository, FavorRepositoryEF>( );
            services.AddScoped<IMasterRepository, MasterRepositoryEF>( );
            services.AddScoped<IRegisterRepository, RegisterRepositoryEF>( );
            services.AddScoped<IUserRepository, UserRepositoryEF>( );
            services.AddScoped<IWorkHoursRepository, WorkHoursRepositoryEF>( );
            return services;
        }

        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RegisterMapperProfile>( );
                cfg.AddProfile<WorkHoursMapperProfile>( );
                cfg.AddProfile<FavorMapperProfile>( );
                cfg.AddProfile<MasterMapperProfile>( );
                cfg.AddProfile<UserMapperProfile>( );
            });
            return services;
        }
    }
}
