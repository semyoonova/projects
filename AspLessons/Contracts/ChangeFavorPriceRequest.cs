using AspLessons.Helpers;
using System.Text.Json.Serialization;

namespace AspLessons.Contracts
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
        [JsonPropertyName("workHoursId")]
        public int WorkHoursId { get; set; }


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

    
