using System.Text.Json.Serialization;

namespace BeautySalon.Contracts
{
    public class CreateRegisterRequest
    {
        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }


        [JsonPropertyName("master")]
        public Master Master { get; set; }


        [JsonPropertyName("favor")]
        public Favor Favor { get; set; }


        [JsonPropertyName("user")]
        public User User { get; set;}
    }


}
