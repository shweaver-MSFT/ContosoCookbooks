using Contoso.Core.Models.Data;

namespace Contoso.Data.Models
{
    internal class IngredientModel : IIngredientModel
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
