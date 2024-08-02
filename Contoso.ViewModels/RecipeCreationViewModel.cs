using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Data.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class RecipeCreationViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<RecipeViewModel> _recipeViewModelFactory;

        public IAsyncRelayCommand CreateRecipeCommand { get; }
        public IRelayCommand NavigateBackCommand { get; }

        private ICookbookModel? _cookbookModel;

        private readonly string _recipeId;

        private string _recipeName;
        public string RecipeName
        {
            get => _recipeName;
            set => OnPropertyChanged(ref _recipeName, value);
        }

        private string _recipeDescription;
        public string RecipeDescription
        {
            get => _recipeDescription;
            set => OnPropertyChanged(ref _recipeDescription, value);
        }

        private TimeSpan _prepTime;
        public TimeSpan PrepTime
        {
            get => _prepTime;
            set => OnPropertyChanged(ref _prepTime, value);
        }

        private TimeSpan _cookTime;
        public TimeSpan CookTime
        {
            get => _cookTime;
            set => OnPropertyChanged(ref _cookTime, value);
        }

        private ObservableCollection<IngredientViewModel> _ingredients;
        public ObservableCollection<IngredientViewModel> Ingredients
        {
            get => _ingredients;
            set => OnPropertyChanged(ref _ingredients, value);
        }

        public RecipeCreationViewModel(INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<RecipeViewModel> recipeViewModelFactory)
        {
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _recipeViewModelFactory = recipeViewModelFactory;

            CreateRecipeCommand = new AsyncRelayCommand(CreateRecipeAsync, () => IsLoaded);
            NavigateBackCommand = new RelayCommand(NavigateBack);

            _recipeId = Guid.NewGuid().ToString();
            _recipeName = string.Empty;
            _recipeDescription = string.Empty;
            _prepTime = TimeSpan.Zero;
            _cookTime = TimeSpan.Zero;
            _ingredients = [];
        }

        public override Task LoadAsync(object? parameter = null, CancellationToken? cancellationToken = null)
        {
            if (parameter is ICookbookModel cookbookModel)
            {
                _cookbookModel = cookbookModel;
            }

            Debug.Assert(_cookbookModel != null);

            return base.LoadAsync();
        }

        public override void Unload()
        {
            _recipeName = string.Empty;
            _recipeDescription = string.Empty;
            _ingredients.Clear();
            base.Unload();
        }

        private async Task CreateRecipeAsync()
        {
            Debug.Assert(_cookbookModel != null);

            try
            {
                RecipeModel recipe = new(_recipeId, _cookbookModel.Id, _recipeName, _recipeDescription, _prepTime, _cookTime);
                await _cookbookDataProvider.AddRecipeAsync(recipe);
                NavigateBack();
            }
            catch (Exception)
            {
                // TODO: Handle creation failure
            }
        }

        private void NavigateBack()
        {
            if (_navigationService.CanGoBack())
            {
                _navigationService.GoBack();
            }
        }
    }
}
