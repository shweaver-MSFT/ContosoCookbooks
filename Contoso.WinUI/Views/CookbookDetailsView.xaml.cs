using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class CookbookDetailsView
    {
        public CookbookDetailsViewModel ViewModel => (CookbookDetailsViewModel)DataContext;

        public CookbookDetailsView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<CookbookDetailsViewModel>();
        }

        private void RecipeListView_ItemClick(object sender, Microsoft.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            ViewModel.NavigateToRecipeDetailsCommand.Execute(e.ClickedItem);
        }
    }
}
