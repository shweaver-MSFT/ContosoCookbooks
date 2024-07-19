namespace Contoso.Core.Models.Authentication
{
    public interface IAuthenticationResult
    {
        AuthenticationResultState State { get; }
        IContosoUser User { get; }
    }
}