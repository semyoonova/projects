using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class RemoveMasterRequest
    {
        
        [JsonPropertyName("masterId")]
        public int Id { get; set; }
    }


}
