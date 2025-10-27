namespace Weather.Application.DTOs.Responses.Details;

public class PressureDetails
{
    public double PressureHpa { get; set; }
    public string Summary { get; set; } = null!;
}