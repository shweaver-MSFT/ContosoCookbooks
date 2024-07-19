using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Services.Navigation;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class LandingViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        public IRelayCommand NavigateToLoginCommand { get; }
        public IRelayCommand NavigateToSettingsCommand { get; }

        public LandingViewModel(IAuthenticationService authenticationService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;

            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
            NavigateToSettingsCommand = new RelayCommand(NavigateToSettings);
        }

        public override async Task LoadAsync(object parameter = null)
        {
            bool loggedIn = await _authenticationService.TrySilentLoginAsync();
            if (loggedIn)
            {
                NavigateToHome();
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            base.Unload();
        }

        private void NavigateToLogin()
        {
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Login));
        }

        private void NavigateToHome()
        {
            if (_authenticationService.IsAuthenticated)
            {
                _navigationService.ClearNavigationStack();
                _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Home));
            }
        }

        private void NavigateToSettings()
        {
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Settings));
        }
    }
}
