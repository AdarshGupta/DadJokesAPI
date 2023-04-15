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

        public override bool Equals(object? obj)
        {
            return obj is JokesSearchResultsDTO dTO &&
                   currentPage == dTO.currentPage &&
                   limit == dTO.limit &&
                   Enumerable.SequenceEqual(results, dTO.results) &&
                   searchTerm == dTO.searchTerm &&
                   totalJokes == dTO.totalJokes &&
                   totalPages == dTO.totalPages;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(currentPage, limit, results, searchTerm, totalJokes, totalPages);
        }
    }
}
