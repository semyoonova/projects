using System.Text.Json.Serialization;

namespace BeautySalon.Contracts
{
    public class RemoveMasterRequest
    {
        
        [JsonPropertyName("masterId")]
        public int Id { get; set; }
    }


}
