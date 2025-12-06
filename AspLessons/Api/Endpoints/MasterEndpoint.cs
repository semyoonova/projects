using AspLessons.Abstractions;
using AspLessons.Contracts;

namespace AspLessons.Api.Endpoints
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
                .RequireAuthorization("admin");

            masterGroup.MapPost("/removefavor", async (HttpContext context, RemoveFavorFromMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveFavorFromMaster(request.FavorId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin");

            masterGroup.MapPost("/addworkhours", async (HttpContext context, AddWorkHoursToMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.AddWorkHoursToMaster(request.WorkHoursId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin");

            masterGroup.MapPost("/removeworkhours", async (HttpContext context, RemoveWorkHoursFromMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveWorkHoursFromMaster(request.WorkHoursId, request.MasterId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin");

            //TODO: добавить везде дто 
            masterGroup.MapPost("/add", async (HttpContext context, CreateMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.AddMaster(request.Name);
                return Results.Ok( );
            })
                .RequireAuthorization("admin");

            masterGroup.MapPost("/remove", async (HttpContext context, RemoveMasterRequest request,
                IMasterService masterService) =>
            {
                await masterService.RemoveMaster(await masterService.FindMasterById(request.Id));
                return Results.Ok( );
            })
                .RequireAuthorization("admin");


            return masterGroup;
        }
    }
}
