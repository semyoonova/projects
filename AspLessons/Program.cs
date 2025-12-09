using AspLessons;
using AspLessons.Abstractions;
using AspLessons.Api.Endpoints;
using AspLessons.Helpers;
using AspLessons.Models;
using AspLessons.Services;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


//TODO: добавить провайдер, захидировать данные 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Scoped);
builder.Services.AddSingleton<CheckHeaderMiddleWare>();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMappers();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});
builder.Logging.AddSeq( apiKey: builder.Configuration["SeqApiKey"]);
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly( ).GetName( ).Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "This is a JWT bearer authentication scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement( )
           {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Name = JwtBearerDefaults.AuthenticationScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
           });
});
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(builder.Configuration["DataBaseConfig:Connection"]);
    options.UseSnakeCaseNamingConvention();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Audience = builder.Configuration["AuthConfig:Audience"];
    options.ClaimsIssuer = builder.Configuration["AuthConfig:Issuer"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["AuthConfig:Issuer"],
        ValidAudience = builder.Configuration["AuthConfig:Audience"],
        RequireExpirationTime = true,
        RequireAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["AuthConfig:IssuerSignKey"]!))
    };
});
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});
builder.Services.AddHttpClient<IPhoneValidator, PhoneValidatorService>(options =>
{
    options.BaseAddress = new Uri("https://apilayer.net");
});


builder.Services.AddAuthorizationBuilder()
    .AddPolicy("client", policy => policy.RequireRole("client"))
    .AddPolicy("admin", policy => policy.RequireRole("admin"));
builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(opt =>
        opt.UseNpgsqlConnection(builder.Configuration["DataBaseConfig:Connection"]));
});
builder.Services.AddHangfireServer();
builder.Services.AddScoped<GetRandomDiscountJob>();
builder.Services.AddScoped<OpenNotificationJob>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("AuthConfig"));
builder.Services.Configure<PhoneValidatorConfig>(builder.Configuration.GetSection("PhoneValidatorConfig"));
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHangfireDashboard();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

var userGroup = app.MapGroup("/user").WithTags("User");
userGroup.MapUser();

var workhoursGroup = app.MapGroup("/workhours").WithTags("Work Hours");
workhoursGroup.MapWorkhours();

var favorGroup = app.MapGroup("/favor").WithTags("Favor");
favorGroup.MapFavor();
var masterGroup = app.MapGroup("/master").WithTags("Master");
masterGroup.MapMaster();

app.MapGet("/", async (context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.SendFileAsync(Path.Combine("wwwroot", "pages", "main", "index.html"));
});
app.UseStaticFiles();
app.UseHttpLogging();

RecurringJob.AddOrUpdate<GetRandomDiscountJob>("RandomDiscount", job => job.SetRandomDicsount( ), Cron.Daily);
RecurringJob.AddOrUpdate<OpenNotificationJob>("OpenNotification", job => job.Notificate( ), "0 9 * * *");


app.Run();
