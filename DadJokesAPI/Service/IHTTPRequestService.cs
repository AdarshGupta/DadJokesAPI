namespace DadJokesAPI.Service
{
    public interface IHTTPRequestService
    {
        public Task<string> makeGETRequest(string relativeUri);
    }
}
