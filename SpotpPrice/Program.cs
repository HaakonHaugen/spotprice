using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotpPrice;

string env = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");

var host = new HostBuilder()

    //.ConfigureFunctionsWorkerDefaults()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddEnvironmentVariables();
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((appBuilder, services) =>
    {
        // Add the HttpClientFactory and configure it to use the named client
        services.AddHttpClient<IStromPrisClient, StromPrisClient>(client =>
        {
            client.BaseAddress = new Uri(appBuilder.Configuration["Hvakosterstrommen:BaseUrl"]);
        });
    })

    .Build();

host.Run();