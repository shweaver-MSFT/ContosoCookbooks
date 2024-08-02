using Contoso.Core.Models.Data;
using Contoso.Core.Services;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class MeasurementViewModel : ViewModelBase
    {
        private readonly ITelemetryService _telemetryService;
        private readonly ILocalizationService _localizationService;

        private string _measurementTypeText;
        public string MeasurementTypeText
        {
            get => _measurementTypeText;
            set => OnPropertyChanged(ref _measurementTypeText, value);
        }

        private string _amountText;
        public string AmountText
        {
            get => _amountText;
            set => OnPropertyChanged(ref _amountText, value);
        }

        public MeasurementViewModel(ITelemetryService telemetryService, ILocalizationService localizationService) 
        { 
            _telemetryService = telemetryService;
            _localizationService = localizationService;

            _measurementTypeText = string.Empty;
            _amountText = string.Empty;
        }

        public override Task LoadAsync(object? parameter = null, CancellationToken? cancellationToken = null)
        {
            Debug.Assert(cancellationToken != null);

            try
            {
                if (parameter is IMeasurementModel measurementModel)
                {
                    AmountText = GetAmountString(measurementModel.Amount);
                    MeasurementTypeText = GetMeasurementTypeString(_localizationService, measurementModel.MeasurementType, measurementModel.Amount);

                    _telemetryService.Log($"MeasurementViewModel loaded: {AmountText} {MeasurementTypeText}");
                }
            }
            catch (Exception)
            {
                // TODO: Handle error state
            }

            return base.LoadAsync();
        }

        public override void Unload()
        {
            _measurementTypeText = string.Empty;
            _amountText = string.Empty;
            base.Unload();
        }

        private static string GetAmountString(double amount)
        {
            double noChange = Math.Truncate(amount);
            double change = amount - noChange;
            if (change > 0)
            {
                string changeString =
                    change == .25 ? "1/4" :
                    change == .5 ? "1/2" :
                    change == .75 ? "3/4" :
                    string.Empty;
                
                if (changeString != string.Empty)
                {
                    if (amount >= 1)
                    {
                        return $"{noChange} {changeString}";
                    }
                    else
                    {
                        return changeString;
                    }
                }
            }

            return amount.ToString();
        }

        private static string GetMeasurementTypeString(ILocalizationService localizationService, MeasurementType measurementType, double amount)
        {
            bool plural = amount > 1;

            return measurementType switch
            {
                MeasurementType.Piece => plural
                    ? localizationService.GetString("MeasurementType_Piece_Plural")
                    : localizationService.GetString("MeasurementType_Piece"),
                MeasurementType.Cup => plural
                    ? localizationService.GetString("MeasurementType_Cup_Plural")
                    : localizationService.GetString("MeasurementType_Cup"),
                MeasurementType.Teaspoon => plural
                    ? localizationService.GetString("MeasurementType_Teaspoon_Plural")
                    : localizationService.GetString("MeasurementType_Teaspoon"),
                MeasurementType.Tablespoon => plural
                    ? localizationService.GetString("MeasurementType_Tablespoon_Plural")
                    : localizationService.GetString("MeasurementType_Tablespoon"),
                MeasurementType.Ounce => plural
                    ? localizationService.GetString("MeasurementType_Ounce_Plural")
                    : localizationService.GetString("MeasurementType_Ounce"),
                MeasurementType.Pound => plural
                    ? localizationService.GetString("MeasurementType_Pound_Plural")
                    : localizationService.GetString("MeasurementType_Pound"),
                MeasurementType.Can => plural
                    ? localizationService.GetString("MeasurementType_Can_Plural")
                    : localizationService.GetString("MeasurementType_Can"),
                MeasurementType.Whole => plural
                    ? localizationService.GetString("MeasurementType_Whole_Plural")
                    : localizationService.GetString("MeasurementType_Whole"),
                _ => plural
                    ? localizationService.GetString("MeasurementType_Unknown_Plural")
                    : localizationService.GetString("MeasurementType_Unknown"),
            };
        }
    }
}
