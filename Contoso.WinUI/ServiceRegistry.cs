using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.Core.Services;
using Contoso.Core.Services.DataProviders;
using Contoso.Data.Mock;
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
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<INotificationService, NotificationService>();
            services.AddSingleton<ICookbookDataProvider, MockCookbookDataProvider>();
            services.AddSingleton<ICancellationService, CancellationService>();

            // Factories
            services.AddSingleton<IFactoryService<CookbookViewModel>, CookbookViewModelFactory>();
            services.AddSingleton<IFactoryService<RecipeViewModel>, RecipeViewModelFactory>();
            services.AddSingleton<IFactoryService<IngredientViewModel>, IngredientViewModelFactory>();
            services.AddSingleton<IFactoryService<MeasurementViewModel>, MeasurementViewModelFactory>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<CookbookDetailsViewModel>();
            services.AddSingleton<RecipeDetailsViewModel>();
            services.AddSingleton<RecipeCreationViewModel>();

            // Configure the default Ioc container
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(serviceProvider);
        }
    }
}
