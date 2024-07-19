using Contoso.Core.Services.DataProviders;
using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class CookbookViewModelFactory : IFactoryService<CookbookViewModel>
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<RecipeViewModel> _recipeViewModelFactory;
        private readonly ICookbookDataProvider _cookbookDataProvider;

        public CookbookViewModelFactory(ITelemetryService telemetryService, IFactoryService<RecipeViewModel> recipeViewModelFactory, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _recipeViewModelFactory = recipeViewModelFactory;
            _cookbookDataProvider = cookbookDataProvider;
        }

        public CookbookViewModel Create()
        {
            return new CookbookViewModel(_telemetryService, _recipeViewModelFactory, _cookbookDataProvider);
        }
    }
}
