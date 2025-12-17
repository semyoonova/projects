using AspLessons.Abstractions;
using AspLessons.Contracts;
using AspLessons.Filters;
using AspLessons.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AspLessons.Api.Endpoints
{
    public static class UserEndpoint
    {
        
        public static IEndpointRouteBuilder MapUser(this IEndpointRouteBuilder userGroup)
        {
            userGroup.MapPost("/register",
            async ( RegisterFromUserRequest request, IUserService userService, 
            IMapper mapper, HttpContext httpContext) =>
            {
                var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var userId = userIdClaim.Value;
                RegisterDto registerDto = mapper.Map<RegisterDto>(request);
                registerDto.UserId = int.Parse(userId);
                Register register = await userService.RegistrationToFavor(registerDto);
                RegisterResponse response = mapper.Map<RegisterResponse>(register);
                return Results.Ok(response);

            })
                .RequireAuthorization("client")
                .WithSummary("Создание записи")
                .WithDescription("Создание записи")
                .Produces<RegisterResponse>(200);



            userGroup.MapPost("/getdates", async (GetDatesRequest request, IUserService userService) =>
            {
                WorkingDatesResponse response = new WorkingDatesResponse()
                { 
                    Dates = await userService.GetWorkingDays(request.MasterId)
                };

                return Results.Ok(response); 
            })
                .RequireAuthorization("client")
                .WithSummary("Получение дней, в которые работает мастер")
                .Produces(statusCode: 200, typeof(WorkingDatesResponse));



            userGroup.MapPost("/getslots", async (GetSlotsRequest request, IUserService userService, IMapper mapper) =>
            {
                SlotsResponse response = new SlotsResponse( )
                {
                    Slots = await userService.GetFreeSlots(mapper.Map<RegisterDto>(request))
                };
                return Results.Ok(response); 
            })
                .RequireAuthorization("client")
                .WithSummary("Получение слотов для записи в конктреный день")
                .Produces<SlotsResponse>(200);

            userGroup.MapGet("/getmasters", async ( IUserService userService) =>
            {
                MastersResponse response = new MastersResponse( )
                {
                    Masters = await userService.GetAllMasters( )
                };
                return Results.Ok(response);
            })
                .RequireAuthorization("user")
                .WithSummary("Получение списка мастеров")
                .Produces<MastersResponse>(200);

            userGroup.MapPost("/getfavors", async ([FromBody] GetFavorsByMasterRequest request,
                [FromServices] IUserService userService, IMapper mapper) =>
            {
                List<Favor> favors = await userService.GetAllFavorsByMaster(request.MasterId);
                FavorsResponse response = new FavorsResponse()
                {
                    Favors = favors.Select(favor => mapper.Map<FavorDto>(favor)).ToList()
                };
                return Results.Ok(response);
            })
                .RequireAuthorization("user")
                .WithSummary("Получение списка услуг конкретного мастера")
                .Produces<FavorsResponse>(200);

            userGroup.MapPost("/getnotaddedfavors", async ([FromBody] GetFavorsByMasterRequest request,
                [FromServices] IUserService userService, IMapper mapper) =>
            {
                List<Favor> favors = await userService.GetNotAddedFavorsByMaster(request.MasterId);
                FavorsResponse response = new FavorsResponse()
                {
                    Favors = favors.Select(favor => mapper.Map<FavorDto>(favor)).ToList()
                };
                return Results.Ok(response);
            })
                .RequireAuthorization("user")
                .WithSummary("Получение списка услуг которых нет у конкретного мастера")
                .Produces<FavorsResponse>(200);

            userGroup.MapGet("/getallfavors", async ([FromServices] IUserService userService, IMapper mapper) =>
            {
                List<Favor> favors = await userService.GetAllFavors();
                FavorsResponse response = new FavorsResponse( )
                {
                    Favors = favors.Select(favor => mapper.Map<FavorDto>(favor)).ToList()
                };
                return Results.Ok(response);
            })
                .RequireAuthorization("admin")
                .WithSummary("Получение списка услуг конкретного мастера")
                .Produces<FavorsResponse>(200);

            userGroup.MapPost("/registration", async (CreateUserRequest request, IUserService userService, IMapper mapper) =>
            {
                await userService.CreateUser(mapper.Map<UserDto>(request));
                return Results.Ok();
            })
                .AddEndpointFilter<ValidationFilter<CreateUserRequest>>()
                .WithSummary("Регистрация");


            userGroup.MapPost("/auth", async (ToLoginRequest request, IUserService userService, IMapper mapper) =>
            {
                AuthResult response = await userService.LoginUser(request.PhoneNumber, request.Password);
                if (response.IsSuccess)
                {
                    return Results.Ok(response);
                    
                }
                return Results.StatusCode(response.Code);
                
            })
                .WithSummary("Вход");

            return userGroup;
        }
    }
}
