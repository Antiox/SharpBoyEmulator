using SharpBoyEmulator.Core;
using SharpBoyEmulator.BLL;
using SharpBoyEmulator.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SharpBoyEmulator.GUI.DepedencyInjection
{
    public class SimpleInjectorConfiguration
    {
        public Container Container { get; } = new Container();


        internal void InitializeContainer()
        {
            Container.Register<ISharpBoyBusinessLogic, SharpBoyBusinessLogic>(Lifestyle.Singleton);
            Container.RegisterInitializer<ISharpBoyBusinessLogic>(controller => controller.Emulator = Container.GetInstance<IEmulator>());
            Container.Register<IEmulator, Emulator>(Lifestyle.Singleton);
            Container.Verify();
        }
    }
}
