using IHttpClientFactory = Weather.Application.Core.IHttpClientFactory;

namespace Weather.Infrastructure;

public abstract class NetworkServiceBase(IHttpClientFactory httpClientFactory) : IDisposable
{
    private bool _disposed;
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected HttpClient CreateHttpClient(string clientName) => httpClientFactory.CreateClient(clientName);
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        _disposed = true;
    }

    ~NetworkServiceBase()
    {
        Dispose(false);
    }
}