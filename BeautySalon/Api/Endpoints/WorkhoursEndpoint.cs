using BeautySalon.Abstractions;
using BeautySalon.Contracts;
using BeautySalon.Models;
using AutoMapper;

namespace BeautySalon.Api.Endpoints
{
    public static class WorkhoursEndpoint
    {
        public static IEndpointRouteBuilder MapWorkhours(this IEndpointRouteBuilder workhoursGroup)
        {

            
            workhoursGroup.MapPost("/add", async (HttpContext context, CreateWorkHoursRequest request, IWorkHoursService workHoursService, IMapper mapper) =>
            {
                var workHoursDto = mapper.Map<WorkHoursDto>(request);
                var workHours = await workHoursService.AddWorkHours(workHoursDto);
                return Results.Ok();
            })
                .RequireAuthorization("admin")
                .WithSummary("Добавление рабочих часов");


            return workhoursGroup;
        }
    }
}
