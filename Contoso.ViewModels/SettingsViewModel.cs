using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Services;

namespace Contoso.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public IRelayCommand NavigateBackCommand { get; }

        public SettingsViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;

            NavigateBackCommand = new RelayCommand(NavigateBack);
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
