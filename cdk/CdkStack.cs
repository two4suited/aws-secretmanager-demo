using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.SecretsManager;
using Amazon.CDK.AWS.Lambda;

namespace Cdk
{
    public class CdkStack : Stack
    {
        internal CdkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var secret = new Secret(this,"Secret", new SecretProps()
            {
                SecretName = "DemoSecret",
                Description = "This is a demo secret key",
                GenerateSecretString = new SecretStringGenerator()
                {
                    SecretStringTemplate = JsonSerializer.Serialize(new { username="sampleuser"}),
                    GenerateStringKey = "password"
                }
            });
            
            var lambda = new Function(this,"SecretFunction", new FunctionProps()
            {
                Runtime = Runtime.DOTNET_CORE_2_1,
                Timeout = Duration.Minutes(1),
                MemorySize = 512,
                Handler = "RetrieveSecrets::RetrieveSecrets.Function::FunctionHandler",
                Code = Code.FromAsset("../src/RetrieveSecrets/bin/Debug/netcoreapp2.1/publish"),
                FunctionName = "SecretDemoFunction"
            });

            secret.GrantRead(lambda);

        }
    }
}
