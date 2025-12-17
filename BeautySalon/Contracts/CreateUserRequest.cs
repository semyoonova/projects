using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class CreateUserRequest
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }


        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }


        [JsonPropertyName("password")]
        public string Password { get; set; }


        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }


}
