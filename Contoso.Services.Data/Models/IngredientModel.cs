using Contoso.Models.Data;

namespace Contoso.Services.Data.Models
{
    public class IngredientModel : IIngredientModel
    {
        public string Id { get; }
        public string ParentId { get; }
        public string Name { get; }
        public IMeasurementModel Measurement { get; }

        public IngredientModel(string id, string parentId, string name, IMeasurementModel measurement)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Measurement = measurement;
        }
    }
}
