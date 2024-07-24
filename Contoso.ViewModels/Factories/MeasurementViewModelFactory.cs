using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class MeasurementViewModelFactory : IFactoryService<MeasurementViewModel>
    {
        private readonly ILocalizationService _localizationService;
        private readonly ITelemetryService _telemetryService;

        public MeasurementViewModelFactory(ILocalizationService localizationService, ITelemetryService telemetryService)
        {
            _localizationService = localizationService;
            _telemetryService = telemetryService;
        }

        public MeasurementViewModel Create()
        {
            return new MeasurementViewModel(_telemetryService, _localizationService);
        }
    }
}
