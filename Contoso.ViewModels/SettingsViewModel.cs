using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Services;
using System.Threading.Tasks;

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

        public override Task LoadAsync(object parameter = null)
        {
            return base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            base.Unload();
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
