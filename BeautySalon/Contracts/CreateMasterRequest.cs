using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class CreateMasterRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }


    }


}
