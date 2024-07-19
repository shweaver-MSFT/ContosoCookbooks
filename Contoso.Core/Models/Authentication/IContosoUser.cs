namespace Contoso.Core.Models.Authentication
{
    public interface IContosoUser
    {
        string DisplayName { get; }
        string Id { get; }
    }
}