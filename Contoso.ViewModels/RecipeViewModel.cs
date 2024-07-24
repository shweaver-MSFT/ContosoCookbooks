using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class RecipeViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;

        private IRecipeModel _model;
        public IRecipeModel Model
        {
            get => _model;
            private set => OnPropertyChanged(ref _model, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            private set => OnPropertyChanged(ref _name, value);
        }

        private ObservableCollection<IngredientViewModel> _ingredients;
        public ObservableCollection<IngredientViewModel> Ingredients
        {
            get => _ingredients;
            private set => OnPropertyChanged(ref _ingredients, value);
        }

        public RecipeViewModel(ITelemetryService telemetryService)
        {
            _telemetryService = telemetryService;

            _ingredients = [];
        }

        public override async Task LoadAsync(object parameter = null, CancellationToken? cancellationToken = null)
        {
            Debug.Assert(cancellationToken != null);

            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;

            if (parameter is IRecipeModel recipe)
            {
                await Task.Delay(2000);
                if (IsCancelled())
                {
                    Unload();
                    return;
                }

                // Recipe meta
                Model = recipe;
                Name = recipe.Name;

                _telemetryService.Log($"RecipeViewModel loaded: {Name}");
            }

            await base.LoadAsync(parameter);
        }

        public override void Unload()
        {
            _name = string.Empty;
            _ingredients = [];
            base.Unload();
        }
    }
}
