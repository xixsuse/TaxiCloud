using Ninject;
using NLog;
using TaxiCloud.Core.Api.Wialon;
using TaxiCloud.Core.Api.Yandex;
using TaxiCloud.Core.IoC;
using TaxiCloud.Core.Repository;

namespace TaxiCloud.Core.Abstract
{
    public abstract class BaseCommand : ICommand
    {
        protected WialonApi WialonApi;
        protected ILogger Logger;
        protected YandexApi YandexApi;
        protected SqlRepository Sql;

        public abstract string Name { get; }

        [Inject]
        public void Initialize(WialonApi api, YandexApi yandexApi, SqlRepository sql)
        {
            WialonApi = api;
            Logger = LogManager.GetCurrentClassLogger();
            YandexApi = yandexApi;
            Sql = sql;
        }

        protected BaseCommand()
        {
            IocKernel.Inject(this);
        }

        public abstract void Execute();
    }
}