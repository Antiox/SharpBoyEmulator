using System.Windows;
using SharpBoyEmulator.Models;
using SharpBoyEmulator.GUI.DepedencyInjection;


namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ISharpBoyBusinessLogic BusinessLogic { get; set; }


        void Application_Startup(object sender, StartupEventArgs args)
        {
            var injector = new SimpleInjectorConfiguration();
            injector.InitializeContainer();
            BusinessLogic = injector.Container.GetInstance<ISharpBoyBusinessLogic>();
        }
    }
}
