using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class RecipeDetailsView
    {
        public RecipeDetailsViewModel ViewModel => (RecipeDetailsViewModel)DataContext;

        public RecipeDetailsView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<RecipeDetailsViewModel>();
        }
    }
}
