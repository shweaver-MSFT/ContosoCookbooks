using Contoso.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Contoso.WinUI.Views
{
    public abstract class PageViewBase : Page
    {
        protected ViewModelBase ViewModelBase => (ViewModelBase)DataContext;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModelBase.LoadAsync(e.Parameter);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModelBase.Unload();
            base.OnNavigatedFrom(e);
        }
    }
}
