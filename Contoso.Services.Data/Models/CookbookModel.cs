using Contoso.Core.Models.Data;

namespace Contoso.Data.Models
{
    public class CookbookModel : ICookbookModel
    {
        public string Id { get; }

        public string Title { get; }

        public CookbookModel(string id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
