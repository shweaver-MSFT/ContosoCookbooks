using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class IngredientViewModelFactory : IFactoryService<IngredientViewModel>
    {
        private readonly ITelemetryService _telemetryService;
        private readonly IFactoryService<MeasurementViewModel> _measurementViewModelFactory;

        public IngredientViewModelFactory(ITelemetryService telemetryService, IFactoryService<MeasurementViewModel> measurementViewModelFactory)
        {
            _telemetryService = telemetryService;
            _measurementViewModelFactory = measurementViewModelFactory;
        }

        public IngredientViewModel Create()
        {
            return new IngredientViewModel(_telemetryService, _measurementViewModelFactory);
        }
    }
}
