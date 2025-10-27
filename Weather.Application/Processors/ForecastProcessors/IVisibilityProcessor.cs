using Weather.Application.DTOs;
using Weather.Application.DTOs.Responses.Details;

namespace Weather.Application.Processors;

public interface IVisibilityProcessor : IForecastProcessor<VisibilityDetails> {}