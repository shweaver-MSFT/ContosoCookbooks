using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class CookbookViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILocalizationService _localizationService;
        private readonly ICookbookDataProvider _cookbookDataProvider;

        private ICookbookModel? _model;
        public ICookbookModel? Model
        {
            get => _model;
            private set => OnPropertyChanged(ref _model, value);
        }

        private string? _title;
        public string? Title
        {
            get => _title;
            private set => OnPropertyChanged(ref _title, value);
        }

        private string? _recipeCountText;
        public string? RecipeCountText
        {
            get => _recipeCountText;
            private set => OnPropertyChanged(ref _recipeCountText, value);
        }

        public CookbookViewModel(ITelemetryService telemetryService, ILocalizationService localizationService, ICookbookDataProvider cookbookDataProvider)
        {
            _telemetryService = telemetryService;
            _localizationService = localizationService;
            _cookbookDataProvider = cookbookDataProvider;
        }

        public override async Task LoadAsync(object? parameter = null, CancellationToken? cancellationToken = null)
        {
            Debug.Assert(cancellationToken != null);
            bool IsCancelled() => cancellationToken.HasValue && cancellationToken.Value.IsCancellationRequested;

            try
            {
                if (parameter is ICookbookModel cookbook)
                {
                    // Cookbook meta
                    Model = cookbook;
                    Title = cookbook.Title;

                    // Get recipe models
                    IList<IRecipeModel> recipes = await _cookbookDataProvider.GetRecipesAsync(cookbook.Id);
                    if (IsCancelled())
                    {
                        Unload();
                        return;
                    }

                    // RecipeCountText
                    string format = _localizationService.GetString("HomeCookbookListItemRecipeCountFormat");
                    RecipeCountText = string.Format(format, recipes.Count);

                    _telemetryService.Log($"CookbookViewModel loaded: {Title}");
                }
            }
            catch (Exception)
            {
                // TODO: Handle error state
            }

            await base.LoadAsync();
        }

        public override void Unload()
        {
            _title = string.Empty;
            _recipeCountText = string.Empty;
            base.Unload();
        }
    }
}
