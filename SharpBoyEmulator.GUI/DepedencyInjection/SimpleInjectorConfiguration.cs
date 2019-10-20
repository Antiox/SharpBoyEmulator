using SharpBoyEmulator.Core;
using SharpBoyEmulator.Models;
using SharpBoyEmulator.BLL;
using SimpleInjector;



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
