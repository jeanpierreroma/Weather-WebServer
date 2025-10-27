using System.Globalization;
using Weather.Infrastructure.Nasa.NasaDTOs;

namespace Weather.Infrastructure.Nasa;

public static class NasaRequestBuilder
{
    public static NasaDialAMoonRequest BuildNasaRequest(DateTime dateTime)
    {
        DateTime utc = dateTime.ToUniversalTime();
        
        return new NasaDialAMoonRequest
        {
            Timestamp = utc.ToString("yyyy-MM-dd'T'HH:mm", CultureInfo.InvariantCulture)
        };
    }
}