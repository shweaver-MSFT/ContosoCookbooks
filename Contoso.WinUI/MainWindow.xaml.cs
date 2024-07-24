using CommunityToolkit.Mvvm.DependencyInjection;
using Contoso.Core.Models.Navigation;
using Contoso.ViewModels;
using Microsoft.UI.Xaml;
using System;

namespace Contoso.WinUI;

public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        this.InitializeComponent();

        ViewModel = Ioc.Default.GetRequiredService<MainViewModel>();
        ViewModel.NavigationRequested += OnNavigationRequested;

        RootFrame.Loaded += RootFrame_Loaded;
    }

    ~MainWindow()
    {
        RootFrame.Loaded -= RootFrame_Loaded;
        ViewModel.NavigationRequested -= OnNavigationRequested;
    }

    private async void RootFrame_Loaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadAsync();
    }

    private void OnNavigationRequested(object? sender, INavigationRequest e)
    {
        Type viewType = ViewRegistry.GetViewType(e.NavigationRouteKey);
        RootFrame.Navigate(viewType, e.Parameter);
    }
}
