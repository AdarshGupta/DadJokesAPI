namespace DadJokesAPI.Models
{
    public class JokeDTO
    {
        public string Id { get; set; }

        public string Joke { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is JokeDTO dTO &&
                   Id == dTO.Id &&
                   Joke == dTO.Joke;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Joke);
        }
    }
}
