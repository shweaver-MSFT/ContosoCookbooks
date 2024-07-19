namespace Contoso.Core.Models.Authentication
{
    public enum AuthenticationResultState
    {
        Success,
        Failure_Unknown,
        Failure_UserNotFound,
        Failure_UserNotAuthorized,
        Failure_UserCredentialsInvalid,
    }
}
