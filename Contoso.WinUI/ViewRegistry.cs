using Contoso.Core.Models.Navigation;
using Contoso.WinUI.Views;
using System;
using System.Collections.Generic;

namespace Contoso.WinUI
{
    internal static class ViewRegistry
    {
        private static readonly IDictionary<NavigationRouteKey, Type> NavigationRouteViewTypes = new Dictionary<NavigationRouteKey, Type>()
        {
            { NavigationRouteKey.Home, typeof(HomeView) },
            { NavigationRouteKey.Settings, typeof(SettingsView) },
            { NavigationRouteKey.Landing, typeof(LandingView) },
            { NavigationRouteKey.Login, typeof(LoginView) },
        };

        public static Type GetViewType(NavigationRouteKey key)
        {
            return NavigationRouteViewTypes[key];
        }
    }
}
