using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class SettingsView
    {
        public SettingsViewModel ViewModel => (SettingsViewModel)DataContext;

        public SettingsView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<SettingsViewModel>();
        }
    }
}
