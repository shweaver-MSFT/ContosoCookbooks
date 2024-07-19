using Contoso.Core.Models.Authentication;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.Authentication
{
    public class MockAuthenticationService : IAuthenticationService
    {
        private static readonly IList<IContosoUser> _users = [
            new ContosoUser("test", "Bob Ferguson")
            ];

        private readonly INavigationService _navigationService;
        private readonly ITelemetryService _telemetryService;
        private IContosoUser _currentUser;

        public IContosoUser CurrentUser => _currentUser;

        public bool IsAuthenticated => _currentUser != null;

        public MockAuthenticationService(INavigationService navigationService, ITelemetryService telemetryService)
        {
            _navigationService = navigationService;
            _telemetryService = telemetryService;
        }

        public async Task<IAuthenticationResult> LoginAsync(IUserCredential credentials, bool staySignedIn = false)
        {
            await Task.CompletedTask;

            if (credentials == null)
            {
                return new AuthenticationResult(AuthenticationResultState.Failure_UserCredentialsInvalid);
            }

            IContosoUser newUser = null;
            try
            {
                if (credentials is UsernamePasswordCredential usernamePasswordCredential)
                {
                    foreach (IContosoUser user in _users)
                    {
                        // Check if the username matches, ignore the password
                        if (user.Id == usernamePasswordCredential.Username)
                        {
                            newUser = user;
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _telemetryService.LogException("Failed to login user with provided credentials", e);

                return new AuthenticationResult(AuthenticationResultState.Failure_Unknown);
            }

            if (newUser == null)
            {
                return new AuthenticationResult(AuthenticationResultState.Failure_UserNotFound);
            }

            if (IsAuthenticated)
            {
                // Log out the existing user.
                Logout();
            }

            // Set the newly logged in user.
            _currentUser = newUser;

            if (staySignedIn)
            {
                // TODO: Cache the user credentials for later.
            }

            return new AuthenticationResult(AuthenticationResultState.Success, newUser);
        }

        public bool Logout()
        {
            // TODO: Clear the user credentials cache so they don't automatically sign in again.
            _currentUser = null;
            _navigationService.ClearNavigationStack();
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Landing));
            return true;
        }

        public Task<bool> TrySilentLoginAsync()
        {
            // TODO: Lookup the cached user creds and attempt to log them back in.
            return Task.FromResult(false);
        }
    }
}
