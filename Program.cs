using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureFunctionsMlbCSharp
{
    public class Program
    {
        public static void Main()
        {
            var builder = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(ConfigureServices);

            var host = builder.Build();

            host.Run();
        }

        private static string rapidApiMlbBaseUrl = Environment.GetEnvironmentVariable("X_RAPIDAPI_HOST") ?? "";

        static void ConfigureServices(HostBuilderContext builder, IServiceCollection services)
        {
            services
                .AddHttpClient("mlb", (provider, client) =>
                {
                    client.BaseAddress = new Uri($"https://{rapidApiMlbBaseUrl}");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Key", Environment.GetEnvironmentVariable("X_RAPIDAPI_KEY"));
                    client.DefaultRequestHeaders.Add("X-RapidAPI-Host", rapidApiMlbBaseUrl);
                });
        }
    }
}