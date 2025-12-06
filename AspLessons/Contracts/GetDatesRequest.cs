using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class GetDatesRequest
    {
        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }
    }

    public class GetSlotsRequest
    {
        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }


        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }


        [JsonPropertyName("date")]
        public DateOnly Date {  get; set; }
    }
}
