using Microsoft.OpenApi.Models;
using Weather.Application;
using Weather.Application.DTOs;
using Weather.Infrastructure;

namespace Weather.Api;

public static class StartupHelperExtension
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        
        // Add OpenAPI (Swagger) support
        builder.Services.AddOpenApi();
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

        builder.Services.AddSingleton<IHttpClientFactory>(_ =>
            new HttpClientFactory(
                new Uri("https://api.open-meteo.com/"),
                c => c.DefaultRequestHeaders.UserAgent.ParseAdd("WeatherApp/1.0")
            ));

        builder.Services.AddScoped<IOpenMeteoClient, OpenMeteoClient>();

        builder.Services.AddScoped<IAirQualityProcessor, AirQualityProcessor>();
        builder.Services.AddScoped<IFeelsLikeProcessor, FeelsLikeProcessor>();
        builder.Services.AddScoped<IUvProcessor, UvProcessor>();
        
        builder.Services.AddScoped<IWeatherService, WeatherService>();
        
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