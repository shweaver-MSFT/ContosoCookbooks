using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.ViewModels.Factories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class RecipeDetailsViewModel :  ViewModelBase
    {
        private readonly ICancellationService _cancellationService;
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<IngredientViewModel> _ingredientViewModelFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public IRelayCommand NavigateBackCommand { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        private ObservableCollection<IngredientViewModel> _ingredients;
        public ObservableCollection<IngredientViewModel> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }

        public RecipeDetailsViewModel(ICancellationService cancellationService, INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<IngredientViewModel> ingredientViewModelFactory)
        {
            _cancellationService = cancellationService;
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _ingredientViewModelFactory = ingredientViewModelFactory;

            _cancellationTokenSource = cancellationService.GetLinkedTokenSource();
            _name = string.Empty;
            _ingredients = [];

            NavigateBackCommand = new RelayCommand(NavigateBack);
        }

        public override async Task LoadAsync(object? parameter = null, CancellationToken? cancellationToken = null)
        {
            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;

            if (cancellationToken == null)
            {
                _cancellationTokenSource = _cancellationService.GetLinkedTokenSource();
                cancellationToken = _cancellationTokenSource.Token;
            }

            if (parameter is IRecipeModel recipe)
            {
                Name = recipe.Name;

                // Get ingredient models
                IList<IIngredientModel> ingredients = await _cookbookDataProvider.GetIngredientsAsync(recipe.Id);
                if (IsCancelled())
                {
                    Unload();
                    return;
                }

                // Create VMs for each model
                foreach (IIngredientModel ingredient in ingredients)
                {
                    IngredientViewModel ingredientVM = _ingredientViewModelFactory.Create();
                    Ingredients.Add(ingredientVM);
                    
                    // Don't wait for the load task
                    _ = ingredientVM.LoadAsync(ingredient, cancellationToken);
                }
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            _ingredients = [];
            base.Unload();
        }

        private void NavigateBack()
        {
            _cancellationTokenSource.Cancel();
            _navigationService.GoBack();
        }
    }
}
