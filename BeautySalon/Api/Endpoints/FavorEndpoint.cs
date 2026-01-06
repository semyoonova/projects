using BeautySalon.Abstractions;
using BeautySalon.Contracts;
using BeautySalon.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Endpoints
{
    public static class FavorEndpoint
    {
        public static IEndpointRouteBuilder MapFavor(this IEndpointRouteBuilder favorGroup)
        {
            favorGroup.MapPost("/add", async (HttpContext context, [FromBody] CreateFavorRequest request,
                [FromServices] IFavorService favorService, IMapper mapper) =>
            {
                FavorDto favorDto = mapper.Map<FavorDto>(request);
                Favor favor = await favorService.AddFavor(favorDto);
                return Results.Ok();
            })
                .RequireAuthorization("admin")
                .WithSummary("Добавление услуги");

            favorGroup.MapPost("/changeprice", async (HttpContext context, [FromBody] ChangeFavorPriceRequest request,
                [FromServices] IFavorService favorService) =>
            {

                Favor favor = await favorService.ChangeFavorPrice(request.FavorId, request.NewPrice);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Изменение стоимости услуги");
            
            favorGroup.MapPost("/remove", async (HttpContext context, [FromBody] RemoveFavorRequest request,
                [FromServices] IFavorService favorService) =>
            {
                await favorService.RemoveFavor(request.FavorId);
                return Results.Ok( );
            })
                .RequireAuthorization("admin")
                .WithSummary("Удаление услуги");


            return favorGroup;
        }
    }
}
