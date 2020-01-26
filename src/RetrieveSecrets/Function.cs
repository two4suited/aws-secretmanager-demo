using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization.Json;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace RetrieveSecrets
{
    
    
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;

        public Function(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Function() 
        {
            _serviceProvider = Startup.Container.BuildServiceProvider();

        }
        public string FunctionHandler(object input, ILambdaContext context)
        {
            string secretName = "DemoSecret";
            var client = _serviceProvider.GetService<IAmazonSecretsManager>();

            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
          
            var response = client.GetSecretValueAsync(request).Result;

            var secrets = JsonConvert.DeserializeObject<AppSecrets>(response.SecretString);

            return secrets.password;
        }
    }
}