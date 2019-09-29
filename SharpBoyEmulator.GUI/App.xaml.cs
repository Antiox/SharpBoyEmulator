using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SharpBoyEmulator.BLL;
using SharpBoyEmulator.Core;
using SharpBoyEmulator.Interfaces;
using SharpBoyEmulator.Models;
using SharpBoyEmulator.GUI.DepedencyInjection;


namespace SharpBoyEmulator.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ISharpBoyBusinessLogic BusinessLogic { get; private set; }

        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            var injector = new SimpleInjectorConfiguration();
            injector.InitializeContainer();

            BusinessLogic = injector.Container.GetInstance<ISharpBoyBusinessLogic>();

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
