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
            new PropertyMetadata(null, new PropertyChangedCallback(OnViewModelChanged)));

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is IngredientListItemView control)
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

        public IngredientListItemView()
        {
            this.InitializeComponent();
            UpdateVisualState();
        }
    }
}
