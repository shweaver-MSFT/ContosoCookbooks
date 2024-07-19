using Contoso.Core.Models.Navigation;
using System;

namespace Contoso.Core.Services
{
    public interface INavigationService
    {
        event EventHandler<INavigationRequest> NavigationRequested;

        void Navigate(INavigationRequest navigationRequest);

        bool CanGoBack();

        void GoBack();

        void ClearNavigationStack();
    }
}
