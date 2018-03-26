using Ninject.Modules;
using NLog;
using TaxiCloud.Core.Api.Wialon;
using TaxiCloud.Core.Api.Yandex;
using TaxiCloud.Core.Repository;

namespace TaxiCloud.Core.IoC
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToConstant(LogManager.GetCurrentClassLogger());
            Bind<SqlRepository>().ToSelf().InTransientScope();
            Bind<WialonApi>().ToSelf().InTransientScope().WithConstructorArgument("token", "ee1d7a981bdbc126ed6a71509fcc949658C0F05803AB98B14C98F081C293F358BAC3CBE3");
            Bind<YandexApi>().ToSelf().InTransientScope().WithConstructorArgument("token", "1a01037e06a744e693b9a56dcff953b1");
            Bind<CoreProccessor>().ToSelf().InSingletonScope();
        }
    }
}