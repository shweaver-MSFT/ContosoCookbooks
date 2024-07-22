using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class IngredientViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILocalizationService _localizationService;

        private string _name;
        public string Name
        {
            get => _name;
            set => OnPropertyChanged(ref _name, value);
        }

        public IngredientViewModel(ILocalizationService localizationService, ITelemetryService telemetryService)
        {
            _localizationService = localizationService;
            _telemetryService = telemetryService;
        }

        public override Task LoadAsync(object parameter = null)
        {
            if (parameter is IIngredientModel ingredient)
            {
                Name = ingredient.Name;
                _telemetryService.Log($"IngredientViewModel loaded: {Name}");
            }

            return base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            base.Unload();
        }
    }
}
