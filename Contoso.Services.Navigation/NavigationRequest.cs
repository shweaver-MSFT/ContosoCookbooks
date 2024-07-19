using Contoso.Core.Models.Navigation;

namespace Contoso.Services.Navigation
{
    public readonly struct NavigationRequest : INavigationRequest
    {
        public NavigationRouteKey NavigationRouteKey { get; }
        public object Parameter { get; }

        public NavigationRequest(NavigationRouteKey navigationRouteKey, object parameter = null)
        {
            NavigationRouteKey = navigationRouteKey;
            Parameter = parameter;
        }
    }
}