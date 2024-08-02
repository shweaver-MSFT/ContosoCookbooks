using Contoso.ViewModels;
using Microsoft.UI.Xaml;
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
            UpdatedVisualState();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModelBase.Unload();
            UpdatedVisualState();
            base.OnNavigatedFrom(e);
        }

        protected virtual void UpdatedVisualState()
        {
            bool isLoaded = ViewModelBase != null && ViewModelBase.IsLoaded;
            VisualStateManager.GoToState(this, isLoaded ? "LoadedState" : "UnloadedState", true);
        }
    }
}
