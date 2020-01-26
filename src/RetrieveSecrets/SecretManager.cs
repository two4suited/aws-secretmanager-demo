using System;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Extensions.Caching;
using Amazon.SecretsManager.Model;
using Newtonsoft.Json;

namespace RetrieveSecrets
{
    public interface ISecretManager
    {
        AppSecrets RetrieveSecrets();
    }
    
    public class SecretManager : ISecretManager,IDisposable
    {
        private readonly SecretsManagerCache _cache;

        public SecretManager(IAmazonSecretsManager amazonSecretsManager)
        {
            _cache = new SecretsManagerCache(amazonSecretsManager);
        }
        public AppSecrets RetrieveSecrets()
        {
            string secretId = "DemoSecret";
            
            var secretResponse = _cache.GetSecretString(secretId).Result;
            
            var secrets = JsonConvert.DeserializeObject<AppSecrets>(secretResponse);

            return secrets;
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}