using BeautySalon.Helpers;
using System.Text.Json.Serialization;

namespace BeautySalon.Contracts
{
    public class ChangeFavorPriceRequest
    {
        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }


        [JsonPropertyName("newPrice")]
        public int NewPrice { get; set; }
    }
    public class RemoveFavorRequest
    {
        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }
    }
    public class AddFavorToMasterRequest
    {
        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }


        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }

    }

    public class AddWorkHoursToMasterRequest
    {
        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [JsonPropertyName("begin")]
        public TimeOnly Begin { get; set; }

        [JsonPropertyName("end")]
        public TimeOnly End { get; set; }

        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }

    }

    public class RemoveFavorFromMasterRequest
    {
        [JsonPropertyName("favorId")]
        public int FavorId { get; set; }


        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }
    }

    public class RemoveWorkHoursFromMasterRequest
{
        [JsonPropertyName("workHoursId")]
        public int WorkHoursId { get; set; }


        [JsonPropertyName("masterId")]
        public int MasterId { get; set; }
    }

}

    
