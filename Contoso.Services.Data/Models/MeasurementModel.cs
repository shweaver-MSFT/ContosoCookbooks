using Contoso.Core.Models.Data;

namespace Contoso.Data.Models
{
    public class MeasurementModel : IMeasurementModel
    {
        public MeasurementType MeasurementType { get; }
        public PreparationType PreparationType { get; }
        public double Amount { get; }

        public MeasurementModel(MeasurementType measurementType, PreparationType preparationType, double amount)
        {
            MeasurementType = measurementType;
            PreparationType = preparationType;
            Amount = amount;
        }
    }
}
