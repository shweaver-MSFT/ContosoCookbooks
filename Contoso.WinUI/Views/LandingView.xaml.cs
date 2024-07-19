using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Contoso.WinUI.Views
{
    public sealed partial class LandingView : Page
    {
        public LandingViewModel ViewModel => (LandingViewModel)DataContext;

        public LandingView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<LandingViewModel>();
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
