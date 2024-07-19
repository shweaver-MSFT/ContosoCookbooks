using System;
using System.Threading.Tasks;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Services.Navigation;

namespace Contoso.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public event EventHandler<INavigationRequest> NavigationRequested;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.NavigationRequested += NavigationService_NavigationRequested;
        }

        ~MainViewModel()
        {
            _navigationService.NavigationRequested -= NavigationService_NavigationRequested;
        }

        private void NavigationService_NavigationRequested(object sender, INavigationRequest e)
        {
            // Forward the request so the view can perform the navigation.
            NavigationRequested?.Invoke(this, e);
        }

        public override Task LoadAsync(object parameter = null)
        {
            // Go to the Landing view.
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Landing));

            return base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            base.Unload();
        }
    }
}
