namespace Weather.Application.Core;

public interface IHttpClientFactory
{
    HttpClient CreateClient(string name);
}