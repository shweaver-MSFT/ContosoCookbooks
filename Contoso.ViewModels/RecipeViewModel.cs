using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<IngredientViewModel> _ingredientViewModelFactory;
        private readonly ICookbookDataProvider _cookbookDataProvider;

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

        public RecipeViewModel(ITelemetryService telemetryService, IFactoryService<IngredientViewModel> ingredientViewModelFactory, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _ingredientViewModelFactory = ingredientViewModelFactory;
            _cookbookDataProvider = cookbookDataProvider;

            _ingredients = [];
        }

        public override async Task LoadAsync(object parameter = null)
        {
            if (parameter is IRecipeModel recipe)
            {
                Name = recipe.Name;

                // Create IngredientViewModels for each IIngredientModel
                var ingredients = await _cookbookDataProvider.GetIngredientsAsync(recipe.Id);
                foreach (IIngredientModel ingredient in ingredients)
                {
                    IngredientViewModel ingredientVM = _ingredientViewModelFactory.Create();
                    await ingredientVM.LoadAsync(ingredient);

                    Ingredients.Add(ingredientVM);
                }

                _telemetryService.Log($"RecipeViewModel loaded: {Name}");
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            _ingredients = [];
            base.Unload();
        }
    }
}
