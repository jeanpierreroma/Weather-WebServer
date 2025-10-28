using Weather.Application.DTOs.Responses.Details;

namespace Weather.Application.Processors;

public interface IMoonProcessor : IForecastProcessor<MoonDetails>
{ }