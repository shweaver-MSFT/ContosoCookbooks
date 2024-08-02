using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.ViewModels;

namespace Contoso.WinUI.Views
{
    public sealed partial class RecipeCreationView
    {
        public RecipeCreationViewModel ViewModel => (RecipeCreationViewModel)DataContext;

        public RecipeCreationView()
        {
            this.InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<RecipeCreationViewModel>();
        }
    }
}
