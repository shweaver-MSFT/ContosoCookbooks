namespace Contoso.Core.Models.Authentication
{
    internal class ContosoUser : IContosoUser
    {
        public string Id { get; }
        public string DisplayName { get; }

        public ContosoUser(string id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }
    }
}
