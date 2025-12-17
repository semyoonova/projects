using System.Text.Json.Serialization;

namespace AspLessons.Contracts
{
    public class CreateFavorRequest
    {
        [JsonPropertyName("favorName")]
        public string FavorName { get; set; }


        [JsonPropertyName("price")]
        public int Price { get; set; }


        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }


}
