using Weather.Application.DTOs.Responses;

namespace Weather.Application.Abstraction;

public interface IMoonProvider
{
    Task<MoonSnapshotDto?> GetMoon(DateTime dateTime, CancellationToken cancellationToken = default);
}