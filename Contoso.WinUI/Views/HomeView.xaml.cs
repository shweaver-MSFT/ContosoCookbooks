using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class HomeView
    {
        public HomeViewModel ViewModel => (HomeViewModel)DataContext;

        public HomeView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<HomeViewModel>();
        }
    }
}
