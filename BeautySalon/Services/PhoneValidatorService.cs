using BeautySalon.Abstractions;
using BeautySalon.Models;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace BeautySalon.Services
{
    public class PhoneValidatorService(HttpClient httpClient, 
        IOptionsMonitor<PhoneValidatorConfig> configOptions) : IPhoneValidator
    {
        private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions( )
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        };
        private PhoneValidatorConfig _phoneValidatorConfig = configOptions.CurrentValue;
        public async Task<PhoneValidationResultDto> ValidatePhone(string phoneNumber)
        {
            var uri = $"/api/validate?access_key={_phoneValidatorConfig.ApiKey}&number={phoneNumber}";
            var result = await httpClient.GetFromJsonAsync<PhoneValidationResultDto>(uri);
            return result!;
        }
    }
}
