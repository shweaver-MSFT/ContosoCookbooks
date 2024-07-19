using System;

namespace Contoso.Core.Models.Data
{
    public interface IRecipeModel
    {
        string Id { get; }
        string ParentId { get; }
        string Name { get; }
        string Description { get; }
        TimeSpan PrepTime { get; }
        TimeSpan CookTime { get; }
    }
}
