namespace Weather.Application.DTOs.Responses.Details;

public class PrecipitationDetails
{
    public double PrecipitationDuringLast24Hours { get; set; }
    public string Summary { get; init; } = null!;
}