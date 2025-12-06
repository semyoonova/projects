using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class ToLoginRequest
    {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }


        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
