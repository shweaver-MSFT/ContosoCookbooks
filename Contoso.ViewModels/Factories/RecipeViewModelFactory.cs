using Contoso.Core.Services;

namespace Contoso.ViewModels.Factories
{
    public class RecipeViewModelFactory : IFactoryService<RecipeViewModel>
    {
        private readonly ITelemetryService _telemetryService;

        public RecipeViewModelFactory(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;
        }

        public RecipeViewModel Create()
        {
            return new RecipeViewModel(_telemetryService);
        }
    }
}
