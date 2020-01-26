using System.Security.Principal;

namespace RetrieveSecrets
{
    public class AppSecrets
    {
        public string username { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return $"Username: {username} Password: {password}";
        }
    }
}