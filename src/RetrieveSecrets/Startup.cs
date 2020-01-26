using System;
using System.IO;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace RetrieveSecrets
{
    public static class Startup
    {
        private static IServiceCollection Container => ConfigureServices(Configuration);

        public static IServiceProvider Provider => ConfigureProvider(Container); 
               
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
            services.AddSingleton<IApplicationService,ApplicationService>();

            services.AddSingleton(provider =>
            {
                var secrets = provider.GetService<ISecretManager>();
                var config = secrets.RetrieveSecrets();
                return config;
            });
            

            services.AddLogging(x =>
            {
                x.AddConsole();
                x.AddAWSProvider();
                x.SetMinimumLevel(LogLevel.Information);
            });
           
            return services;
        }

        private static IServiceProvider ConfigureProvider(IServiceCollection serviceCollection)
        {   
            return Container.BuildServiceProvider();
        }
    }
}