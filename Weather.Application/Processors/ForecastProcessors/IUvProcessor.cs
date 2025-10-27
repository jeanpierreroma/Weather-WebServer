using Weather.Application.DTOs;
using Weather.Application.DTOs.Responses.Details;

namespace Weather.Application.Processors;

public interface IUvProcessor : IForecastProcessor<UvDetails> {}