using Autofac.Extensions.DependencyInjection;
using Schedule.Worker.Configurations;

namespace Schedule.Worker;

public class Program
{
    public static void Main(string[] args)
    {

        Console.WriteLine($"Starting up with environment:{GetHostEnvironment(args)}");

        try
        {
            var builder = CreateHostBuilder(args).Build();

            builder.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureAppConfiguration((hostingContext, configuration) =>
        {
            configuration.AddJsonFile($"appsettings.{GetHostEnvironment(args)}.json", optional: true,
                reloadOnChange: true);
        })
        .ConfigureServices((hostingContext, services) =>
        {

            AppConfigurator.ConfigServices(services);

            AppConfigurator.ConfigQuartz(services, hostingContext.Configuration);

        });



    private static string GetHostEnvironment(string[] commandLineArguments)
    {
        var defaultEnvironment = Environments.Development;

        var aspnetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (aspnetCoreEnvironment != null)
        {
            return GetValidEnvironmentString(aspnetCoreEnvironment) ?? defaultEnvironment;
        }

        var dotnetEnvironment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        if (dotnetEnvironment != null)
        {
            return GetValidEnvironmentString(dotnetEnvironment) ?? defaultEnvironment;
        }


        return defaultEnvironment;
    }

    private static string GetValidEnvironmentString(string environment)
    {
        var environments = new[]
            {Environments.Development, Environments.Staging, Environments.Production};

        return environments.FirstOrDefault(env => EqualIgnoreCase(environment, env));
    }

    private static bool EqualIgnoreCase(string source, string value)
    {
        return (source != null && value != null && string.Equals(source, value, StringComparison.OrdinalIgnoreCase))
               || (source == null && value == null);
    }
}