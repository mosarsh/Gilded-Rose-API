using GildedRose.BL.Auth;
using GildedRose.BL.Logics;
using Ninject.Modules;

namespace GildedRose.BL.DI
{
    /// <summary>
    /// Service Ninject Class inhereted by NinjectModule to be compile by NInject and add binding for DI
    /// </summary>
    public class ServiceNinjectModule : NinjectModule
    {
        /// <summary>
        /// Overrding Load method and adding baindings for DI to take into account
        /// </summary>
        public override void Load()
        {
            Bind<IInventoryLogic>().To<InventoryLogic>();
            Bind<IPurchaseLogic>().To<PurchaseLogic>();
            Bind<IUserLogic>().To<UserLogic>();
            Bind<IJwtAuthManager>().To<JwtAuthManager>();
        }
    }
}
