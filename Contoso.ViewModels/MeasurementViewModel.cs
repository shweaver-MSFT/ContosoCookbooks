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
        }

        public override Task LoadAsync(object parameter = null, CancellationToken? cancellationToken = null)
        {
            Debug.Assert(cancellationToken != null);

            if (parameter is IMeasurementModel measurementModel)
            {
                AmountText = GetAmountString(measurementModel.Amount);
                MeasurementTypeText = GetMeasurementTypeString(measurementModel.MeasurementType, measurementModel.Amount);
            }

            return base.LoadAsync(parameter, cancellationToken);
        }

        public override void Unload()
        {
            _measurementTypeText = null;
            _amountText = null;
            base.Unload();
        }

        private string GetAmountString(double amount)
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
                        return $"{noChange.ToString()} {changeString}";
                    }
                    else
                    {
                        return changeString;
                    }
                }
            }

            return amount.ToString();
        }

        private string GetMeasurementTypeString(MeasurementType measurementType, double amount)
        {
            bool plural = amount > 1;

            switch (measurementType)
            {
                case MeasurementType.Piece:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Piece_Plural")
                        : _localizationService.GetString("MeasurementType_Piece");
                case MeasurementType.Cup:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Cup_Plural")
                        : _localizationService.GetString("MeasurementType_Cup");
                case MeasurementType.Teaspoon:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Teaspoon_Plural")
                        : _localizationService.GetString("MeasurementType_Teaspoon");
                case MeasurementType.Tablespoon:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Tablespoon_Plural")
                        : _localizationService.GetString("MeasurementType_Tablespoon");
                case MeasurementType.Ounce:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Ounce_Plural")
                        : _localizationService.GetString("MeasurementType_Ounce");
                case MeasurementType.Pound:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Pound_Plural")
                        : _localizationService.GetString("MeasurementType_Pound");
                case MeasurementType.Can:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Can_Plural")
                        : _localizationService.GetString("MeasurementType_Can");
                case MeasurementType.Whole:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Whole_Plural")
                        : _localizationService.GetString("MeasurementType_Whole");
                default:
                    return plural
                        ? _localizationService.GetString("MeasurementType_Unknown_Plural")
                        : _localizationService.GetString("MeasurementType_Unknown");
            }
        }
    }
}
