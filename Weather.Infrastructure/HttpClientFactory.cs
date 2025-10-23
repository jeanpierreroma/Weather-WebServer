using System.Collections.Concurrent;
using System.Net.Http.Headers;
using IHttpClientFactory = Weather.Application.Core.IHttpClientFactory;

namespace Weather.Infrastructure;

public sealed class HttpClientFactory : IHttpClientFactory
{
    private const string MediaType = "application/json";
    
    private readonly Uri _baseAddress;
    private readonly Action<HttpClient>? _configure;
    private readonly ConcurrentDictionary<string, HttpClient> _cache = new();

    public HttpClientFactory(Uri baseAddress, Action<HttpClient>? configure = null)
    {
        _baseAddress = baseAddress;
        _configure = configure;
    }

    public HttpClient CreateClient(string name)
    {
        return _cache.GetOrAdd(name, _ =>
        {
            var handler = new SocketsHttpHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };

            var client = new HttpClient(handler, disposeHandler: true)
            {
                BaseAddress = _baseAddress,
                Timeout = TimeSpan.FromSeconds(30)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            client.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0");
            
            _configure?.Invoke(client);
            
            return client;
        });
    }
}