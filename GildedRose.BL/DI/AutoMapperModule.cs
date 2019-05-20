using AutoMapper;
using GildedRose.DL;
using GildedRose.DTO;
using Ninject;
using Ninject.Modules;

namespace GildedRose.BL.DI
{
    /// <summary>
    /// This is to Instantiate AutoMapper Instance with DI in constructor  
    /// </summary>
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().ToMethod(AutoMapper).InSingletonScope();
        }

        private IMapper AutoMapper(Ninject.Activation.IContext context)
        {
            Mapper.Initialize(config =>
            {
                config.ConstructServicesUsing(type => context.Kernel.Get(type));

                config.CreateMap<Inventory, InventoryModel>();
                config.CreateMap<User, UserResponseModel>();
                config.CreateMap<User, UserIdentityModel>(MemberList.None);
            });

            Mapper.AssertConfigurationIsValid();
            return Mapper.Instance;
        }
    }
}
