using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILocalizationService _localizationService;
        private readonly IFactoryService<MeasurementViewModel> _measurementViewModelFactory;

        private string _name;
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        private MeasurementViewModel _measurement;
        public MeasurementViewModel Measurement
        {
            get => _measurement;
            set => OnPropertyChanged(ref _measurement, value);
        }

        public IngredientViewModel(ILocalizationService localizationService, ITelemetryService telemetryService, IFactoryService<MeasurementViewModel> measurementViewModelFactory)
        {
            _localizationService = localizationService;
            _telemetryService = telemetryService;
            _measurementViewModelFactory = measurementViewModelFactory;
        }

        public override async Task LoadAsync(object parameter = null, CancellationToken? cancellationToken = null)
        {
            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;

            if (parameter is IIngredientModel ingredient)
            {
                await Task.Delay(2000);
                if (IsCancelled())
                {
                    Unload();
                    return;
                }

                // Ingredient meta
                Name = ingredient.Name;

                // Create a MeasurementViewModel and load it.
                Measurement = _measurementViewModelFactory.Create();
                await Measurement.LoadAsync(ingredient.Measurement, cancellationToken);

                _telemetryService.Log($"IngredientViewModel loaded: {Name}");
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            base.Unload();
        }
    }
}
