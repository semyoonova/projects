using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class CreateWorkHoursRequest
    {
        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }


        [JsonPropertyName("begin")]
        public TimeOnly Begin { get; set; }


        [JsonPropertyName("end")]
        public TimeOnly End { get; set; }


    }
}
