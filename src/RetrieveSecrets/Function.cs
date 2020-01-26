using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Extensions.DependencyInjection;

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
        public string FunctionHandler(string input, ILambdaContext context)
        {
            return input?.ToUpper();
        }
    }
}