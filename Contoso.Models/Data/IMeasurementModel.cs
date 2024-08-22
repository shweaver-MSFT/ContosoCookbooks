namespace Contoso.Models.Data
{
    public interface IMeasurementModel
    {
        public MeasurementType MeasurementType { get; }
        public PreparationType PreparationType { get; }
        public double Amount { get; }
    }
}
