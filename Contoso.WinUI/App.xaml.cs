using Microsoft.UI.Xaml;

namespace Contoso.WinUI
{
    public partial class App : Application
    {
        private Window m_window;

        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            ServiceRegistry.ConfigureServices();

            m_window = new MainWindow();
            m_window.Activate();
        }
    }
}
