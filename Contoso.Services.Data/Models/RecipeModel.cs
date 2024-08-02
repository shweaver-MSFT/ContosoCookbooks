using Contoso.Core.Models.Data;
using System;

namespace Contoso.Data.Models
{
    public class RecipeModel : IRecipeModel
    {
        public string Id { get; }
        public string ParentId { get; }
        public string Name { get; }
        public string Description { get; }
        public TimeSpan PrepTime { get; }
        public TimeSpan CookTime { get; }

        public RecipeModel(string id, string parentId, string name, string description, TimeSpan prepTime, TimeSpan cookTime)
        {
            Id = id;
            ParentId = parentId;
            Name = name;
            Description = description;
            PrepTime = prepTime;
            CookTime = cookTime;
        }
    }
}
