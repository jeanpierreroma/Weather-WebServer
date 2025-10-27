using Weather.Application.DTOs;

namespace Weather.Application.Processors;

public interface IHumidityProcessor : IForecastProcessor<HumidityDetails> {}