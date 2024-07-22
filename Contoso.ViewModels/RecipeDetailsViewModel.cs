using CommunityToolkit.Mvvm.Input;
using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.ViewModels.Factories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class RecipeDetailsViewModel :  ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;
        private readonly IFactoryService<IngredientViewModel> _ingredientViewModelFactory;

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

        public RecipeDetailsViewModel(INavigationService navigationService, ICookbookDataProvider cookbookDataProvider, IFactoryService<IngredientViewModel> ingredientViewModelFactory)
        {
            _navigationService = navigationService;
            _cookbookDataProvider = cookbookDataProvider;
            _ingredientViewModelFactory = ingredientViewModelFactory;

            NavigateBackCommand = new RelayCommand(NavigateBack);

            _ingredients = [];
        }

        public override async Task LoadAsync(object parameter = null)
        {
            if (parameter is RecipeViewModel recipe)
            {
                Name = recipe.Name;

                IList<IIngredientModel> ingredients = await _cookbookDataProvider.GetIngredientsAsync(recipe.Model.Id);
                foreach (IIngredientModel ingredient in ingredients)
                {
                    IngredientViewModel ingredientVM = _ingredientViewModelFactory.Create();
                    await ingredientVM.LoadAsync(ingredient);
                    Ingredients.Add(ingredientVM);
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
            _navigationService.GoBack();
        }
    }
}
