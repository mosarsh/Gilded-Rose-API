using GildedRose.DL.Repository;
using GildedRose.DL.UoW;
using Ninject.Modules;

namespace GildedRose.DL.DI
{
    public class RepoNinjectModule : NinjectModule
    {
        /// <summary>
        /// Overriding load method to register DI
        /// </summary>
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IInventoryRepository>().To<InventoryRepository>();
            Bind<IPurchaseInventoryRepository>().To<PurchaseInventoryRepository>();
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
