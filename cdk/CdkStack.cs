using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.CDK;
using Amazon.CDK.AWS.SecretsManager;

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
            
            
            
        }
    }
}
