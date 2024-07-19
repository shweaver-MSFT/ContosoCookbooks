using Contoso.Core.Models.Authentication;

namespace Contoso.Services.Authentication
{
    public class UsernamePasswordCredential : IUserCredential
    {
        public string Username { get; }
        public string Password { get; }

        public UsernamePasswordCredential(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
