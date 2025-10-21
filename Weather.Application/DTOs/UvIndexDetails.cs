namespace Weather.Application.DTOs;

public class UvIndexDetails
{
    public double UvIndexValueMax { get; set; }
    public string UvIndexRiskCategoryName { get; set; } = null!;
    public string Summary { get; set; } = null!;
}