using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class CookbookViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<RecipeViewModel> _recipeViewModelFactory;
        private readonly ICookbookDataProvider _cookbookDataProvider;

        private string _title;
        public string Title
        {
            get => _title;
            set => OnPropertyChanged(ref _title, value);
        }

        private ObservableCollection<RecipeViewModel> _recipes;
        public ObservableCollection<RecipeViewModel> Recipes
        {
            get => _recipes;
            set => OnPropertyChanged(ref _recipes, value);
        }

        public CookbookViewModel(ITelemetryService telemetryService, IFactoryService<RecipeViewModel> recipeViewModelFactory, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _recipeViewModelFactory = recipeViewModelFactory;
            _cookbookDataProvider = cookbookDataProvider;

            _recipes = [];
        }

        public override async Task LoadAsync(object parameter = null)
        {
            if (parameter is ICookbookModel cookbook)
            {
                Title = cookbook.Title;

                // Create RecipeViewModels for each IRecipeModel
                var recipes = await _cookbookDataProvider.GetRecipesAsync(cookbook.Id);
                foreach (IRecipeModel recipe in recipes)
                {
                    RecipeViewModel recipeVM = _recipeViewModelFactory.Create();
                    await recipeVM.LoadAsync(recipe);

                    Recipes.Add(recipeVM);
                }

                _telemetryService.Log($"CookbookViewModel loaded: {Title}");
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _title = string.Empty;
            _recipes = [];
            base.Unload();
        }
    }
}
