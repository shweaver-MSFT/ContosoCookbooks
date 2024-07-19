using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class IngredientViewModelFactory : IFactoryService<IngredientViewModel>
    {
        private readonly ILocalizationService _localizationService;
        private readonly ITelemetryService _telemetryService;

        public IngredientViewModelFactory(ILocalizationService localizationService, ITelemetryService telemetryService)
        {
            _localizationService = localizationService;
            _telemetryService = telemetryService;
        }

        public IngredientViewModel Create()
        {
            return new IngredientViewModel(_localizationService, _telemetryService);
        }
    }
}
