namespace RetrieveSecrets
{
    public interface ISecretManager
    {
        AppSecrets RetrieveSecrets();
    }
}