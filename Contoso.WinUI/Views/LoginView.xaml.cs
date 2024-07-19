using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class LoginView
    {
        public LoginViewModel ViewModel => (LoginViewModel)DataContext;

        public LoginView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<LoginViewModel>();
        }
    }
}
