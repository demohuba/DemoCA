using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace JEPCO.Infrastructure.Extensions
{
    public static class LoggingExtension
    {

        public static void RegisterLogging(this IServiceCollection services, WebApplicationBuilder builder)
        {

            ConfigureLogging(builder.Configuration);
            builder.Host.UseSerilog();

        }
        static void ConfigureLogging(ConfigurationManager configuration)
        {
            var environment = configuration.GetValue<string>("Environment");
            Console.WriteLine($"Environment: {environment}");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            var elasticURI = configuration.GetValue<string>("ElasticConfiguration:Uri");
            var username = configuration.GetValue<string>("ElasticConfiguration:Username");
            var password = configuration.GetValue<string>("ElasticConfiguration:Password");

            return new ElasticsearchSinkOptions(new Uri(elasticURI))
            {
                AutoRegisterTemplate = true,
                //IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                IndexFormat = $"{"JEPCO-API"}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                ModifyConnectionSettings = x => x.BasicAuthentication(username, password)

            };
        }
    }
}
