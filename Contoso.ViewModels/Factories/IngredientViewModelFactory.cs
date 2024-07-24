using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class IngredientViewModelFactory : IFactoryService<IngredientViewModel>
    {
        private readonly ILocalizationService _localizationService;
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<MeasurementViewModel> _measurementViewModelFactory;

        public IngredientViewModelFactory(ILocalizationService localizationService, ITelemetryService telemetryService, IFactoryService<MeasurementViewModel> measurementViewModelFactory)
        {
            _localizationService = localizationService;
            _telemetryService = telemetryService;
            _measurementViewModelFactory = measurementViewModelFactory;
        }

        public IngredientViewModel Create()
        {
            return new IngredientViewModel(_localizationService, _telemetryService, _measurementViewModelFactory);
        }
    }
}
