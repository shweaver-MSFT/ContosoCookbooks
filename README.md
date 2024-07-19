# ContosoCookbooks
A simple app demonstrating MVVM architecture with DI pattern

## MVVM w/ IoC + DI
The app is setup to use traditional Model-View-ViewModel (MVVM) architecture, supported by the Microsoft.Toolkit.MVVM package.

Check out the service configuration in `Contoso.WinUI.ServiceRegistry`. The registry connects interfaces with implementations for all of the services, factories, and ViewModels used in the app.

To see the DI pattern in action, notice how services and ViewModels are created. You won't see `new` being used to create any ViewModels or Services, with the exception of factories. This is because all required instances are passed in auto-magically (using DI). When requesting a service or other entity from the default IoC container, we can provide the interface and get back the instance without needing to pass in any of the services that object may require. Services are created lazily on first request and the instances are persisted in the DI container are re-served whenever that service type is re-requested. This setup ensures that singleton service instances are created methodically and maintained consistently. Consuming ViewModels and services do not need to care where their dependencies are sourced from. 

The `HomeViewModel` is the first page a user sees after they've logged in, and it displays the full catalog of cookbooks, contained recipes, and their many ingredients. Look at the `LoadAsync` method to see how `ICookbookModels` are retrieved from the `ICookbookDataProvider`. ViewModels are created for each model using the `IServiceFactory<CookbookViewModel>` (AKA `CookbookViewModelFactory`). The chain of creation is then passed down through ViewModels, `CookbookViewModel` -> `RecipeViewModel` -> `IngredientViewModel`. A nice aspect of this is that `CookbookViewModel` does not need to be concerned with the construction requirements for either Recipes or Ingredients. The DI/factory pattern ensures that newly created ViewModels always have their dependencies sourced from the DI container.

## Loading and Unloading ViewModels
...

## Commanding with RelayCommand
...
