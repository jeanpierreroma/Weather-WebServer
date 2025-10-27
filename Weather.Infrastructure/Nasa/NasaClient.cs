using System.Text.Json;
using Weather.Application.Abstraction;
using Weather.Application.DTOs.Responses;
using Weather.Application.Networking;
using Weather.Infrastructure.Nasa.Mappers;
using Weather.Infrastructure.Nasa.NasaDTOs;

namespace Weather.Infrastructure.Nasa;

public class NasaClient(IHttpClientFactory httpClientFactory)
    : NetworkServiceBase(httpClientFactory), IMoonProvider
{
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public async Task<MoonSnapshotDto?> GetMoon(DateTime date, CancellationToken cancellationToken = default)
    {
        NasaDialAMoonRequest request = NasaRequestBuilder.BuildNasaRequest(date);
        NasaDialAMoonResponse? response = await PerformFetchingMoonInformation(request, cancellationToken);
        
        MoonSnapshotDto? snapshot = response is null
            ? null
            : NasaMapper.MapNasaDialAMoonResponseToMoonSnapshot(response);
        
        return snapshot;
    }

    private async Task<NasaDialAMoonResponse?> PerformFetchingMoonInformation(
        NasaDialAMoonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            HttpClient client = CreateHttpClient("nasa-svs");
            string url = $"{NetworkConfig.DialAMoon}/{request.Timestamp}";
            
            using HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<NasaDialAMoonResponse>(stream, JsonOptions,
                cancellationToken);
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to fetch moon info. Error: {e.Message}", e);
        }
    }
}