using System.IO;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RetrieveSecrets
{
    public static class Startup
    {
        public static IServiceCollection Container => ConfigureServices(Configuration); 
               
        private static IConfigurationRoot Configuration => new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        private static IServiceCollection ConfigureServices(IConfigurationRoot root)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAmazonSecretsManager, AmazonSecretsManagerClient>();
            services.AddSingleton<ISecretManager, SecretManager>();
            
            services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddAWSProvider();
                x.SetMinimumLevel(LogLevel.Information);
            });
           
            return services;
        
        }
    }
}