using Contoso.ViewModels;
using Microsoft.UI.Xaml;

namespace Contoso.WinUI.Views.Controls
{
    public sealed partial class IngredientListItem
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(IngredientViewModel),
            typeof(IngredientListItem),
            new PropertyMetadata(null, new PropertyChangedCallback(OnViewModelChanged)));

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is IngredientListItem control)
            {
                if (e.OldValue is IngredientViewModel oldVM)
                {
                    control.UnregisterEvents(oldVM);
                }

                if (e.NewValue is IngredientViewModel newVM)
                {
                    control.RegisterForEvents(newVM);
                }
            }
        }

        private void RegisterForEvents(IngredientViewModel vm)
        {
            vm.PropertyChanged += OnViewModelPropertyChanged;
            UpdateVisualState();
        }

        private void UnregisterEvents(IngredientViewModel vm)
        {
            vm.PropertyChanged -= OnViewModelPropertyChanged;
            UpdateVisualState();
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IngredientViewModel.IsLoaded))
            {
                UpdateVisualState();
            }
        }

        private void UpdateVisualState()
        {
            string state = ViewModel != null && ViewModel.IsLoaded ? "LoadedState" : "UnloadedState";
            VisualStateManager.GoToState(this, state, true);
        }

        public IngredientViewModel ViewModel
        {
            get => (IngredientViewModel)GetValue(ViewModelProperty);
            set => OnPropertyChanged(ViewModelProperty, value);
        }

        public IngredientListItem()
        {
            this.InitializeComponent();
            UpdateVisualState();
        }
    }
}
