using Contoso.Core.Services.DataProviders;
using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class CookbookViewModelFactory : IFactoryService<CookbookViewModel>
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILocalizationService _localizationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;

        public CookbookViewModelFactory(ITelemetryService telemetryService, ILocalizationService localizationService, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _localizationService = localizationService;
            _cookbookDataProvider = cookbookDataProvider;
        }

        public CookbookViewModel Create()
        {
            return new CookbookViewModel(_telemetryService, _localizationService, _cookbookDataProvider);
        }
    }
}
