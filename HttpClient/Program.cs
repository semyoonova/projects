using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient( );
builder.Services.AddOpenApi("internal");
var app = builder.Build( );
if (app.Environment .IsDevelopment() )
{
    app.MapOpenApi("/openapi/{documentName}/openapi.json");
}
JsonSerializerOptions options = new( ) { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
app.MapGet("/", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    HttpRequestMessage httpRequestMessage = new(HttpMethod.Get, "https://api.chucknorris.io/jokes/random");
    var client = httpClientFactory.CreateClient();
    var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
    httpResponseMessage.EnsureSuccessStatusCode();
    var response = await httpResponseMessage.Content.ReadAsStringAsync();
    var joke = JsonSerializer.Deserialize<Joke>(response, options);
    return Results.Ok(joke.Value);
});


app.Run( );

public class Joke
{
    public string Value { get; set; }
}
