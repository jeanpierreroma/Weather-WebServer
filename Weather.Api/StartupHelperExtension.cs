using Microsoft.OpenApi.Models;
using Weather.Application;
using Weather.Application.Abstraction;
using Weather.Application.Aggregations;
using Weather.Application.Processors;
using Weather.Application.Services;
using Weather.Infrastructure;
using Weather.Infrastructure.Nasa;
using Weather.Infrastructure.OpenMeteo;
using Weather.Infrastructure.Processors;

namespace Weather.Api;

public static class StartupHelperExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        // Add OpenAPI (Swagger) support
        builder.Services.AddEndpointsApiExplorer();
        
        // Add Swagger
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Weather API",
                Version = "v1",
                Description = "API for managing weather forecast",
                Contact = new OpenApiContact
                {
                    Name = "Roman Chornyi",
                    Email = "support@weather.com"
                }
            });
        });

        builder.Services.AddHttpClient("open-meteo-api", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://api.open-meteo.com/");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0");
        });

        builder.Services.AddHttpClient("air-quality-api", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://air-quality-api.open-meteo.com/");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0");
        });
        
        builder.Services.AddHttpClient("nasa-svs", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://svs.gsfc.nasa.gov/api/");
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0");
        });

        builder.Services.AddScoped<IAirQualityProcessor, AirQualityProcessor>();
        builder.Services.AddScoped<IFeelsLikeProcessor, FeelsLikeProcessor>();
        builder.Services.AddScoped<IHumidityProcessor, HumidityProcessor>();
        builder.Services.AddScoped<IPrecipitationProcessor, PrecipitationProcessor>();
        builder.Services.AddScoped<IPressureProcessor, PressureProcessor>();
        builder.Services.AddScoped<IUvProcessor, UvProcessor>();
        builder.Services.AddScoped<IVisibilityProcessor, VisibilityProcessor>();
        builder.Services.AddScoped<IMoonProcessor, MoonProcessor>();
        
        builder.Services.AddScoped<IForecastAggregator, ForecastAggregator>();
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        
        builder.Services.AddScoped<IForecastProvider, OpenMeteoClient>();
        builder.Services.AddScoped<IMoonProvider, NasaClient>();
        
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.MapOpenApi();
            
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API v1");
            });

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        return app;
    }
}