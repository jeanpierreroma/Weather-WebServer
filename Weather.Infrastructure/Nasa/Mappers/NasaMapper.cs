using Weather.Application.DTOs.Responses;
using Weather.Infrastructure.Nasa.NasaDTOs;

namespace Weather.Infrastructure.Nasa.Mappers;

public static class NasaMapper
{
    public static MoonSnapshotDto MapNasaDialAMoonResponseToMoonSnapshot(NasaDialAMoonResponse nasaDialAMoonResponse)
    {
        MoonSnapshotDto snapshot = new MoonSnapshotDto()
        {
            ImageUrl = nasaDialAMoonResponse.Image.Url,
            Time = nasaDialAMoonResponse.Time,
            Phase = nasaDialAMoonResponse.Phase,
            Obscuration = nasaDialAMoonResponse.Obscuration,
            Age = nasaDialAMoonResponse.Age,
            Diameter = nasaDialAMoonResponse.Diameter,
            Distance = nasaDialAMoonResponse.Distance,
            J2000Ra = nasaDialAMoonResponse.J2000Ra,
            J2000Dec = nasaDialAMoonResponse.J2000Dec,
            SubsolarLon = nasaDialAMoonResponse.SubsolarLon,
            SubsolarLat = nasaDialAMoonResponse.SubsolarLat,
            SubearthLon = nasaDialAMoonResponse.SubearthLon,
            SubearthLat = nasaDialAMoonResponse.SubearthLat,
            Posangle = nasaDialAMoonResponse.Posangle,
        };

        return snapshot;
    }

}