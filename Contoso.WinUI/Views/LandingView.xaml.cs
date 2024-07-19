using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class LandingView
    {
        public LandingViewModel ViewModel => (LandingViewModel)DataContext;

        public LandingView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<LandingViewModel>();
        }
    }
}
