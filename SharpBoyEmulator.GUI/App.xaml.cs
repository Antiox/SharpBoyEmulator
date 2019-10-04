using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SharpBoyEmulator.BLL;
using SharpBoyEmulator.Core;
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
