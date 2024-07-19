using Contoso.Core.Models.Authentication;

namespace Contoso.Services.Authentication
{
    public class AuthenticationResult : IAuthenticationResult
    {
        public AuthenticationResultState State { get; }

        public IContosoUser User { get; }

        public AuthenticationResult(AuthenticationResultState state, IContosoUser user = null)
        {
            State = state;
            User = user;
        }
    }
}
