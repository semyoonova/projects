using BeautySalon.Abstractions;
using BeautySalon.Contracts;
using BeautySalon.Models;
using BeautySalon.Services;
using AutoMapper;

namespace BeautySalon.Api.Endpoints
{
    public static class MasterEndpoint
    {
        public static IEndpointRouteBuilder MapMaster(this IEndpointRouteBuilder masterGroup)
        {
            masterGroup.MapPost("/addfavor", async (HttpContext context, AddFavorToMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.AddFavorToMaster(request.FavorId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Добавление услуги мастеру"); 


            masterGroup.MapPost("/removefavor", async (HttpContext context, RemoveFavorFromMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveFavorFromMaster(request.FavorId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Удаление услуги у мастера");

            masterGroup.MapPost("/addworkhours", async (HttpContext context, AddWorkHoursToMasterRequest request,
                IMasterService masterService, IWorkHoursService workHoursService, IMapper mapper) =>
            {
                WorkHoursDto workHours = mapper.Map<WorkHoursDto>(request);
                await masterService.AddWorkHoursToMaster(workHours, request.MasterId);
                return Results.Ok();
            })
                .RequireAuthorization("admin")
                .WithSummary("Добавление рабочих часов мастеру");

            masterGroup.MapPost("/removeworkhours", async (HttpContext context, RemoveWorkHoursFromMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveWorkHoursFromMaster(request.WorkHoursId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Удаление рабочих часов у мастера");

            
            masterGroup.MapPost("/add", async (HttpContext context, CreateMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.AddMaster(request.Name);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Добавление мастера");

            masterGroup.MapPost("/remove", async (HttpContext context, RemoveMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveMaster(await masterService.FindMasterById(request.Id));
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Удаление мастера");


            return masterGroup;
        }
    }
}
