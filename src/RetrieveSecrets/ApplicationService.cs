using Microsoft.Extensions.Options;

namespace RetrieveSecrets
{
    public interface IApplicationService
    {
        string Combine();
    }

    public class ApplicationService : IApplicationService
    {
        private readonly AppSecrets _secrets;

        public ApplicationService(AppSecrets secrets)
        {
            _secrets = secrets;
        }
        public string Combine()
        {
            return _secrets.ToString();
        }
    }
}