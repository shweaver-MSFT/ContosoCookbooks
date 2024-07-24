using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Services.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class CookbookDetailsViewModel :  ViewModelBase
    {
        private readonly ICancellationService _cancellationService;
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<RecipeViewModel> _recipeViewModelFactory;

        private CancellationTokenSource _cancellationTokenSource;

        public IRelayCommand AddRecipeCommand { get; }
        public IRelayCommand EditRecipeCommand { get; }
        public IRelayCommand NavigateBackCommand { get; }
        public IRelayCommand<RecipeViewModel> NavigateToRecipeDetailsCommand { get; }

        private string _cookbookTitle;
        public string CookbookTitle
        {
            get => _cookbookTitle;
            set => OnPropertyChanged(ref _cookbookTitle, value);
        }

        private ObservableCollection<RecipeViewModel> _recipes;
        public ObservableCollection<RecipeViewModel> Recipes
        {
            get => _recipes;
            set => OnPropertyChanged(ref _recipes, value);
        }

        public CookbookDetailsViewModel(ICancellationService cancellationService, INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<RecipeViewModel> recipeViewModelFactory)
        {
            _cancellationService = cancellationService;
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _recipeViewModelFactory = recipeViewModelFactory;

            _cancellationTokenSource = cancellationService.GetLinkedTokenSource();
            _recipes = [];

            AddRecipeCommand = new RelayCommand(AddRecipe, () => IsLoaded);
            EditRecipeCommand = new RelayCommand(EditRecipe, () => IsLoaded);
            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateToRecipeDetailsCommand = new RelayCommand<RecipeViewModel>(NavigateToRecipeDetails, (vm) => vm.IsLoaded);
        }

        public override async Task LoadAsync(object parameter = null, CancellationToken? cancellationToken = null)
        {
            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;
            if (cancellationToken == null)
            {
                _cancellationTokenSource = _cancellationService.GetLinkedTokenSource();
                cancellationToken = _cancellationTokenSource.Token;
            }

            if (parameter is ICookbookModel cookbookModel)
            {
                // Title
                CookbookTitle = cookbookModel.Title;

                // Get recipe models
                IList<IRecipeModel> recipes = await _cookbookDataProvider.GetRecipesAsync(cookbookModel.Id);
                if (IsCancelled())
                {
                    Unload();
                    return;
                }

                // Create VMs for each model
                foreach (IRecipeModel recipe in recipes)
                {
                    RecipeViewModel recipeVM = _recipeViewModelFactory.Create();
                    Recipes.Add(recipeVM);

                    // Don't wait for the load task
                    _ = recipeVM.LoadAsync(recipe, cancellationToken);
                }
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _cookbookTitle = string.Empty;
            _recipes = [];

            base.Unload();
        }

        private void AddRecipe()
        {

        }

        private void EditRecipe()
        {

        }

        private void NavigateBack()
        {
            _navigationService.GoBack();
        }

        private void NavigateToRecipeDetails(RecipeViewModel recipe)
        {
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.RecipeDetails, recipe.Model));
        }
    }
}
