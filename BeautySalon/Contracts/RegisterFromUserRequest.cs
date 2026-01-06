using System.Text.Json.Serialization;

namespace BeautySalon.Contracts
{
    public class RegisterFromUserRequest
    {
        


        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }


        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }


        [JsonPropertyName("date")]
        public DateOnly Date {  get; set; }


        [JsonPropertyName("time")]
        public TimeOnly Time {  get; set; }

    }
}
