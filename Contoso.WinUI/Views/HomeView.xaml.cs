using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;
using Microsoft.UI.Xaml.Controls;

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

        private void CookbooksListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.NavigateToCookbookDetailsCommand.Execute(e.ClickedItem);
        }
    }
}
