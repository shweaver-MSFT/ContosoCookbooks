using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;

        private string _name;
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        private readonly MeasurementViewModel _measurement;
        public MeasurementViewModel Measurement => _measurement;

        public IngredientViewModel(ITelemetryService telemetryService, IFactoryService<MeasurementViewModel> measurementViewModelFactory)
        {
            _telemetryService = telemetryService;

            _name = string.Empty;
            _measurement = measurementViewModelFactory.Create();
        }

        public override async Task LoadAsync(object? parameter = null, CancellationToken? cancellationToken = null)
        {
            Debug.Assert(cancellationToken != null);

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

                // Load the measurement
                await Measurement.LoadAsync(ingredient.Measurement, cancellationToken);
                if (IsCancelled())
                {
                    Unload();
                    return;
                }

                _telemetryService.Log($"IngredientViewModel loaded: {Name}");
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            _measurement.Unload();
            base.Unload();
        }
    }
}
