using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Services.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly ICancellationService _cancellationService;
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<CookbookViewModel> _cookbookViewModelFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public IRelayCommand NavigateToCookbookDetailsCommand { get; }
        public IRelayCommand NavigateToCookbookCreationCommand { get; }
        public IRelayCommand NavigateToSettingsCommand { get; }

        private ObservableCollection<CookbookViewModel> _cookbooks;
        public ObservableCollection<CookbookViewModel> Cookbooks
        {
            get => _cookbooks;
            private set => OnPropertyChanged(ref _cookbooks, value);
        }

        public HomeViewModel(ICancellationService cancellationService, INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<CookbookViewModel> cookbookViewModelFactory)
        {
            _cancellationService = cancellationService;
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _cookbookViewModelFactory = cookbookViewModelFactory;

            _cancellationTokenSource = cancellationService.GetLinkedTokenSource();
            _cookbooks = [];

            NavigateToCookbookDetailsCommand = new RelayCommand<CookbookViewModel>(NavigateToCookbookDetails, (vm) => vm.IsLoaded);
            NavigateToCookbookCreationCommand = new RelayCommand(NavigateToCookbookCreation);
            NavigateToSettingsCommand = new RelayCommand(NavigateToSettings);
        }

        public override async Task LoadAsync(object parameter = null, CancellationToken? cancellationToken = null)
        {
            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;
            if (cancellationToken == null)
            {
                _cancellationTokenSource = _cancellationService.GetLinkedTokenSource();
                cancellationToken = _cancellationTokenSource.Token;
            }

            // Get cookbook models
            IList<ICookbookModel> cookbooks = await _cookbookDataProvider.GetCookbooksAsync();
            if (IsCancelled())
            {
                Unload();
                return;
            }

            // Create VMs for each model
            foreach (ICookbookModel cookbook in cookbooks)
            {
                CookbookViewModel cookbookVM = _cookbookViewModelFactory.Create();
                Cookbooks.Add(cookbookVM);

                // Don't wait for the load task
                _ = cookbookVM.LoadAsync(cookbook, cancellationToken);
            }

            if (IsCancelled())
            {
                Unload();
                return;
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _cancellationTokenSource.Cancel();
            _cookbooks = [];
            base.Unload();
        }

        private void NavigateToCookbookDetails(CookbookViewModel cookbook)
        {
            _cancellationTokenSource.Cancel();
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.CookbookDetails, cookbook.Model));
        }

        private void NavigateToCookbookCreation()
        {
            _cancellationTokenSource.Cancel();
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.CookbookCreation));
        }

        private void NavigateToSettings()
        {
            _cancellationTokenSource.Cancel();
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.Settings));
        }
    }
}
