using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Services.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class CookbookDetailsViewModel :  ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<RecipeViewModel> _recipeViewModelFactory;

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

        public CookbookDetailsViewModel(INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<RecipeViewModel> recipeViewModelFactory)
        {
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _recipeViewModelFactory = recipeViewModelFactory;

            AddRecipeCommand = new RelayCommand(AddRecipe);
            EditRecipeCommand = new RelayCommand(EditRecipe);
            NavigateBackCommand = new RelayCommand(NavigateBack);
            NavigateToRecipeDetailsCommand = new RelayCommand<RecipeViewModel>(NavigateToRecipeDetails);

            _recipes = [];
        }

        public override async Task LoadAsync(object parameter = null)
        {
            if (parameter is CookbookViewModel cookbookViewModel)
            {
                // Title
                CookbookTitle = cookbookViewModel.Title;

                // Get recipe models
                IList<IRecipeModel> recipes = await _cookbookDataProvider.GetRecipesAsync(cookbookViewModel.Model.Id);
                foreach (IRecipeModel recipe in recipes)
                {
                    RecipeViewModel recipeVM = _recipeViewModelFactory.Create();
                    await recipeVM.LoadAsync(recipe);
                    Recipes.Add(recipeVM);
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
            _navigationService.Navigate(new NavigationRequest(NavigationRouteKey.RecipeDetails, recipe));
        }
    }
}
