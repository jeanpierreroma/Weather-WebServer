using System.Text.Json.Serialization;

namespace Weather.Infrastructure.Nasa.NasaDTOs;

public class NasaDialAMoonResponse
{
    [JsonPropertyName("image")]
    public NasaDialAMoonImage Image { get; set; } = null!;
    
    [JsonPropertyName("image_highres")]
    public NasaDialAMoonImage ImageHighres { get; set; } = null!;
    
    [JsonPropertyName("su_image")]
    public NasaDialAMoonImage SuImage { get; set; } = null!;
    
    [JsonPropertyName("su_image_highres")]
    public NasaDialAMoonImage SuImageHighres { get; set; } = null!;
    
    [JsonPropertyName("time")]
    public string Time { get; set; } = null!;
    
    [JsonPropertyName("phase")]
    public double Phase { get; set; }
    
    [JsonPropertyName("obscuration")]
    public double Obscuration { get; set; }
    
    [JsonPropertyName("age")]
    public double Age { get; set; }
    
    [JsonPropertyName("diameter")]
    public double Diameter { get; set; }
    
    [JsonPropertyName("distance")]
    public double Distance { get; set; }
    
    [JsonPropertyName("j2000_ra")]
    public double J2000Ra { get; set; }
    
    [JsonPropertyName("j2000_dec")]
    public double J2000Dec { get; set; }
    
    [JsonPropertyName("subsolar_lon")]
    public double SubsolarLon { get; set; }
    
    [JsonPropertyName("subsolar_lat")]
    public double SubsolarLat { get; set; }
    
    [JsonPropertyName("subearth_lon")]
    public double SubearthLon { get; set; }
    
    [JsonPropertyName("subearth_lat")]
    public double SubearthLat { get; set; }
    
    [JsonPropertyName("posangle")]
    public double Posangle { get; set; }
}