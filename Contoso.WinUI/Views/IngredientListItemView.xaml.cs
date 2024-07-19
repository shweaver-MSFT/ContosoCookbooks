using Contoso.ViewModels;
using Microsoft.UI.Xaml;

namespace Contoso.WinUI.Views
{
    public sealed partial class IngredientListItemView
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(IngredientViewModel),
            typeof(IngredientListItemView),
            new PropertyMetadata(null));

        public IngredientViewModel ViewModel
        {
            get => (IngredientViewModel)GetValue(ViewModelProperty);
            set
            {
                SetValue(ViewModelProperty, value);
                RaisePropertyChanged();
            }
        }

        public IngredientListItemView()
        {
            this.InitializeComponent();
        }
    }
}
