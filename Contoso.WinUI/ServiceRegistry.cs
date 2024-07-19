using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Data.Mock;
using Contoso.Services.Authentication;
using Contoso.Services.Navigation;
using Contoso.ViewModels;
using Contoso.ViewModels.Factories;
using Contoso.WinUI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Contoso.WinUI
{
    internal static class ServiceRegistry
    {
        public static void ConfigureServices()
        {
            ServiceCollection services = new();

            // Services
            services.AddSingleton<ITelemetryService, TelemetryService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<ILocalizationService, LocalizationService>();
            services.AddSingleton<IAuthenticationService, MockAuthenticationService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ICookbookDataProvider, MockCookbookDataProvider>();

            // Factories
            services.AddSingleton<IFactoryService<CookbookViewModel>, CookbookViewModelFactory>();
            services.AddSingleton<IFactoryService<RecipeViewModel>, RecipeViewModelFactory>();
            services.AddSingleton<IFactoryService<IngredientViewModel>, IngredientViewModelFactory>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LandingViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<HomeViewModel>();

            // Configure the default Ioc container
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(serviceProvider);
        }
    }
}
