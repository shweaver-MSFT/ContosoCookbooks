using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;

namespace Contoso.ViewModels.Factories
{
    public class RecipeViewModelFactory : IFactoryService<RecipeViewModel>
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<IngredientViewModel> _ingredientViewModelFactory;
        private readonly ICookbookDataProvider _cookbookDataProvider;

        public RecipeViewModelFactory(ITelemetryService telemetryService, IFactoryService<IngredientViewModel> ingredientViewModelFactory, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _ingredientViewModelFactory = ingredientViewModelFactory;
            _cookbookDataProvider = cookbookDataProvider;
        }

        public RecipeViewModel Create()
        {
            return new RecipeViewModel(_telemetryService, _ingredientViewModelFactory, _cookbookDataProvider);
        }
    }
}
