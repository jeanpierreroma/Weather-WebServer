# Weather-WebServer

This repository contains a small ASP.NET Core solution that provides weather-related data via a web API. It demonstrates a layered architecture (API, Application, Domain, Infrastructure) and uses an external OpenMeteo client for forecasts.

## Architecture overview

- Weather.Api: ASP.NET Core Web API project exposing endpoints (controllers) and wiring DI.
- Weather.Application: Application-facing services, interfaces, DTOs and processing abstractions.
- Weather.Domain: Domain entities and value objects.
- Weather.Infrastructure: Implementations for external clients, processors, and the concrete WeatherService.

The solution is intended as a simple reference app for integrating weather data providers and organizing processing logic into small, testable components.

## Build & run (Windows)

Prerequisites: .NET SDK 6.0+ installed and available on PATH.

1. Open a terminal in the repository root (where `Weather-WebServer.sln` lives).
2. Restore and build the solution:

	dotnet restore; dotnet build

3. Run the API project (from the solution root):

	dotnet run --project Weather.Api

By default the API uses the launch settings in `Weather.Api/Properties/launchSettings.json`. For local development set environment variables or edit `appsettings.Development.json` as needed.

## Development notes

- The code intentionally separates contracts (interfaces) in `Weather.Application` from implementations in `Weather.Infrastructure` to make the system easier to test and replace providers.
- External HTTP calls are performed via `OpenMeteoClient` in `Weather.Infrastructure`.
- Processors like `UvProcessor` and `AirQualityProcessor` encapsulate domain-specific transformation logic.

## Contribution / PR guidance

- This repository is organized as a simple example; avoid changing public interfaces without a clear migration path.
- Keep changes small and focused. Add unit tests for behavior changes when possible.
- If you add functionality that requires configuration (API keys, endpoints), prefer using `appsettings.{Environment}.json` and environment overrides.

---

If you'd like, I can now create small documentation commits or add a CONTRIBUTING.md — tell me which you'd prefer next.

Projects:
- Weather.Api — ASP.NET Core Web API
- Weather.Application — Application interfaces and DTOs
- Weather.Infrastructure — HTTP client and service implementations
- Weather.Domain — Domain types and value objects

```markdown
# Weather WebServer

Small example .NET solution that provides weather data from Open-Meteo.

Projects
- `Weather.Api` — ASP.NET Core Web API
- `Weather.Application` — Application interfaces and DTOs
- `Weather.Infrastructure` — HTTP client and service implementations
- `Weather.Domain` — Domain types and value objects

Prerequisites
- .NET SDK 7.0 or later
- (Optional) An API key or configuration for any external services — see `appsettings.json`

Build

From the repository root:

```powershell
dotnet build Weather-WebServer.sln
```

Run (local)

Start the API from the repository root:

```powershell
dotnet run --project Weather.Api
```

By default the ASP.NET Core app will listen on the ports configured in `Weather.Api/Properties/launchSettings.json` or the environment variables. Use a browser or curl to call the weather endpoints (example: `GET /weatherforecast`).

Configuration
- The app reads configuration from `appsettings.json` and environment variables. For local development, edit `Weather.Api/appsettings.Development.json` or set environment variables.

CI
- A simple GitHub Actions workflow (dotnet.yml) is provided in `.github/workflows` to build the solution on push and PR.

Notes
- This example uses the Open-Meteo API as a data source. If the API requires keys or additional configuration, update `appsettings.json` or provide environment variables before running.
- No unit tests are included. Consider adding tests under a `tests/` folder and enabling them in CI.

License
- This repository does not include a license file. Add a LICENSE file if you intend to publish the project.

```
