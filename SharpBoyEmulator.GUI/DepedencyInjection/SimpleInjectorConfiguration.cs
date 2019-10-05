using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using SharpBoyEmulator.BLL;
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
            Container.RegisterInitializer<ISharpBoyBusinessLogic>(controller => controller.Device = Container.GetInstance<IGameBoy>());
            Container.Register<IGameBoy, GameBoy>(Lifestyle.Singleton);
            Container.Verify();
        }
    }
}
