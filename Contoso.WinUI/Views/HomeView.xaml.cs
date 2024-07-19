using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Contoso.WinUI.Views
{
    public sealed partial class HomeView : Page
    {
        public HomeViewModel ViewModel => (HomeViewModel)DataContext;

        public HomeView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.LoadAsync(e.Parameter);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.Unload();
            base.OnNavigatedFrom(e);
        }
    }
}
