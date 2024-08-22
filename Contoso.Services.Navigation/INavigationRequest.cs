﻿namespace Contoso.Models.Navigation
{
    public interface INavigationRequest
    {
        NavigationRouteKey NavigationRouteKey { get; }
        object? Parameter { get; }
    }
}