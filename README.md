# Weather WebServer

Small example .NET solution that provides weather data from Open-Meteo.

Projects:
- Weather.Api — ASP.NET Core Web API
- Weather.Application — Application interfaces and DTOs
- Weather.Infrastructure — HTTP client and service implementations
- Weather.Domain — Domain types and value objects

Build

Ensure you have the .NET SDK installed (recommended 7.0+).

From the repository root:

```powershell
dotnet build Weather-WebServer.sln
```

Run

From the `Weather.Api` project folder:

```powershell
dotnet run --project Weather.Api
```

Notes

- This project depends on the Open-Meteo API. Configure any API keys or settings in `appsettings.json` or environment variables if needed.
- Tests and CI are not included in this example.
