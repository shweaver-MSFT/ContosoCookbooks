using Contoso.Core.Models.Authentication;
using System.Threading.Tasks;

namespace Contoso.Core.Services
{
    public interface IAuthenticationService
    {
        bool IsAuthenticated { get; }

        Task<IAuthenticationResult> LoginAsync(IUserCredential credentials, bool staySignedIn = false);

        bool Logout();

        Task<bool> TrySilentLoginAsync();
    }
}
