using Contoso.Core.Models.Navigation;
using Contoso.Core.Services;
using System;
using System.Collections.Generic;

namespace Contoso.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public event EventHandler<INavigationRequest>? NavigationRequested;

        private readonly Stack<INavigationRequest> _navigationStack;

        public NavigationService(Stack<INavigationRequest>? navigationStack = null)
        {
            _navigationStack = navigationStack ?? new Stack<INavigationRequest>();
        }

        public void Navigate(INavigationRequest navigationRequest)
        {
            _navigationStack.Push(navigationRequest);
            NavigationRequested?.Invoke(this, navigationRequest);
        }

        public bool CanGoBack()
        {
            return _navigationStack.Count > 1;
        }

        public void GoBack()
        {
            // Remove the current view from the stack
            _navigationStack.Pop();

            // Peek at the top of the stack and navigate to it.
            INavigationRequest request = _navigationStack.Peek();
            NavigationRequested?.Invoke(this, request);
        }

        public void ClearNavigationStack()
        {
            _navigationStack.Clear();
        }
    }
}
