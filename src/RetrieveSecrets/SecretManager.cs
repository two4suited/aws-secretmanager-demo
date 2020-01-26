using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace RetrieveSecrets
{
    public class SecretManager : ISecretManager
    {
        private readonly IAmazonSecretsManager _amazonSecretsManager;

        public SecretManager(IAmazonSecretsManager amazonSecretsManager)
        {
            _amazonSecretsManager = amazonSecretsManager;
        }
        public AppSecrets RetrieveSecrets()
        {
            string secretName = "DemoSecret";
            
            GetSecretValueRequest request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
          
            var response = _amazonSecretsManager.GetSecretValueAsync(request).Result;

            var secrets = JsonConvert.DeserializeObject<AppSecrets>(response.SecretString);

            return secrets;
        }
    }
}