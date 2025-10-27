using System.Text.Json.Serialization;

namespace Weather.Infrastructure.Nasa.NasaDTOs;

public class NasaDialAMoonImage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;
    
    [JsonPropertyName("filename")]
    public string Filename { get; set; } = null!;
    
    [JsonPropertyName("media_type")]
    public string MediaType { get; set; } = null!;
    
    [JsonPropertyName("alt_text")]
    public string AltText { get; set; } = null!;
    
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    [JsonPropertyName("height")]
    public int Height { get; set; }
    
    [JsonPropertyName("pixels")]
    public int Pixels { get; set; }
}