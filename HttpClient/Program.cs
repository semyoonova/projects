using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

app.MapGet("/phonevalidation", async (IHttpClientFactory httpClientFactory) =>
{
    var phoneNumber = "+79003205537";
    var apiKey = "43868fb87263b710b6149353ba1c49cf";
    var url = $"https://apilayer.net/api/validate?access_key={apiKey}&number={phoneNumber}";
    HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, url);
    var client = httpClientFactory.CreateClient( );
    var httpResponseMessage = await client.SendAsync(httpRequestMessage);
    httpResponseMessage.EnsureSuccessStatusCode( );
    var response = await httpResponseMessage.Content.ReadAsStringAsync( );
    var result = JsonSerializer.Deserialize<PhoneValidationResult>(response);
    return Results.Ok(result);
});

app.Run( );

public class Joke
{
    public string Value { get; set; }
}

public static class Validation
{
    public static async Task<bool> PhoneValidation(string phoneNumber, IHttpClientFactory httpClientFactory)
    {
        var apiKey = "43868fb87263b710b6149353ba1c49cf";
        var url = $"https://apilayer.net/api/validate?access_key={apiKey}&number={phoneNumber}";
        HttpRequestMessage httpRequestMessage = new(HttpMethod.Post, url);
        var client = httpClientFactory.CreateClient();
        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
        httpResponseMessage.EnsureSuccessStatusCode();
        var response = await httpResponseMessage.Content.ReadAsStringAsync( );
        var result = JsonSerializer.Deserialize<PhoneValidationResult>(response);
        return result.valid;
    }

}

public class PhoneValidationResult
{
    public bool valid {  get; set; }
    public string number { get; set; }
    public string location { get; set; }
    public string line_type { get; set; }


    // "valid": true,
    //"number": "79003205537",
    //"local_format": "9003205537",
    //"international_format": "+79003205537",
    //"country_prefix": "+7",
    //"country_code": "RU",
    //"country_name": "Russian Federation",
    //"location": "Tatarstan Republic",
    //"carrier": "LLC T2 Mobile",
    //"line_type": "mobile"
}

