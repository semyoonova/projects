using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class RemoveMasterRequest
    {
        
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }


}
