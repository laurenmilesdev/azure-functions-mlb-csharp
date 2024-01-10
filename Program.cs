using AzureFunctionsMlbCSharp;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

string RapidApiMlbBaseUrl = Environment.GetEnvironmentVariable("X_RAPIDAPI_HOST") ?? "";

void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
{
    services.AddApplicationInsightsTelemetryWorkerService();
    services.ConfigureFunctionsApplicationInsights();
    services
        .AddHttpClient("mlb", (provider, client) =>
        {
            client.BaseAddress = new Uri($"https://{RapidApiMlbBaseUrl}");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("X_RAPIDAPI_KEY"));
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", RapidApiMlbBaseUrl);
        });
    services.AddScoped<IMlbService, MlbService>();
}

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(ConfigureServices)
    .Build();

host.Run();