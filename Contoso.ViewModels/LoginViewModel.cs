using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Authentication;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Services.Authentication;
using Contoso.Services.Navigation;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public IAsyncRelayCommand LoginCommand { get; }
        public IRelayCommand NavigateBackCommand { get; }

        public LoginViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            LoginCommand = new AsyncRelayCommand(LoginAsync);
            NavigateBackCommand = new RelayCommand(NavigateBack);
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => OnPropertyChanged(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => OnPropertyChanged(ref _password, value);
        }

        public override Task LoadAsync(object parameter = null)
        {
            Username = "test"; // TODO: Remove this
            return base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _username = string.Empty;
            _password = string.Empty;
            base.Unload();
        }

        private async Task LoginAsync()
        {
            IUserCredential credential = new UsernamePasswordCredential(_username, _password);

            IAuthenticationResult result = await _authenticationService.LoginAsync(credential);
            if (result.State == AuthenticationResultState.Success)
            {
                _navigationService.ClearNavigationStack();
                _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Home));
            }
            else
            {
                // Show an error message because the login failed.
            }
        }

        private void NavigateBack()
        {
            if (_navigationService.CanGoBack())
            {
                _navigationService.GoBack();
            }
        }
    }
}
