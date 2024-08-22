namespace Contoso.Models.Data
{
    public interface IIngredientModel
    {
        string Id { get; }
        string ParentId { get; }
        string Name { get; }
        IMeasurementModel Measurement { get; }
    }
}
