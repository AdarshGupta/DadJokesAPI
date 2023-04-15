using System.Text.Json.Serialization;

namespace DadJokesAPI.Models
{
    public class JokesSearchResultsDTO
    {
        [JsonPropertyName("current_page")]
        public int currentPage { get; set; }
        
        [JsonPropertyName("limit")]
        public int limit { get; set; }

        [JsonPropertyName("results")]
        public List<JokeDTO> results { get; set; }

        [JsonPropertyName("search_term")]
        public string searchTerm { get; set; }

        [JsonPropertyName("total_jokes")]
        public int totalJokes { get; set; }
        
        [JsonPropertyName("total_pages")]
        public int totalPages { get; set; }
    }
}
