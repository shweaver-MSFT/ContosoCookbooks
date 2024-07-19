using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Services.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<CookbookViewModel> _cookbookViewModelFactory;

        public IRelayCommand LogoutCommand { get; }
        public IRelayCommand NavigateToSettingsCommand { get; }

        private ObservableCollection<CookbookViewModel> _cookbooks;
        public ObservableCollection<CookbookViewModel> Cookbooks
        {
            get => _cookbooks;
            set => OnPropertyChanged(ref _cookbooks, value);
        }

        public HomeViewModel(IAuthenticationService authenticationService, INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<CookbookViewModel> cookbookViewModelFactory)
        {
            _authenticationService = authenticationService;
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _cookbookViewModelFactory = cookbookViewModelFactory;

            _cookbooks = [];

            LogoutCommand = new RelayCommand(Logout);
            NavigateToSettingsCommand = new RelayCommand(NavigateToSettings);
        }

        public override async Task LoadAsync(object parameter = null)
        {
            // Get cookbook models
            IList<ICookbookModel> cookbooks = await _cookbookDataProvider.GetCookbooksAsync();

            // Create and load recipe VMs
            foreach (ICookbookModel cookbook in cookbooks)
            {
                CookbookViewModel cookbookVM = _cookbookViewModelFactory.Create();
                await cookbookVM.LoadAsync(cookbook);

                Cookbooks.Add(cookbookVM);
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _cookbooks = [];
            base.Unload();
        }

        private void Logout()
        {
            _authenticationService.Logout();
        }

        private void NavigateToSettings()
        {
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Settings));
        }
    }
}
