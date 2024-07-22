using Contoso.ViewModels;
using Microsoft.UI.Xaml;
using System;

namespace Contoso.WinUI.Views
{
    public sealed partial class IngredientListItemView
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(IngredientViewModel),
            typeof(IngredientListItemView),
            new PropertyMetadata(null, new PropertyChangedCallback(OnViewModelPropertyChanged)));

        private static void OnViewModelPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {

            }
        }

        public IngredientViewModel ViewModel
        {
            get => (IngredientViewModel)GetValue(ViewModelProperty);
            set => OnPropertyChanged(ViewModelProperty, value);
        }

        public IngredientListItemView()
        {
            this.InitializeComponent();
            VisualStateManager.GoToState(this, "Unloaded", true);
        }
    }
}
