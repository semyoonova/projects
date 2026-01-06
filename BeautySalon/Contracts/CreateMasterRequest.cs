using System.Text.Json.Serialization;

namespace BeautySalon.Contracts
{
    public class CreateMasterRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }


    }


}
