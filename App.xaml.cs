using System.Windows;

namespace VittaTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Locator.Init();

            base.OnStartup(e);
        }
    }
}
