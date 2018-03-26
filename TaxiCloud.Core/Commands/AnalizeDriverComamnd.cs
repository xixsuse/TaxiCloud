using System;
using System.Threading;
using TaxiCloud.Core.Abstract;
using TaxiCloud.Core.IoC;
using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.Core.Commands
{
    public class AnalizeDriverComamnd : BaseCommand
    {
        private readonly Driver _driver;
        private readonly Car _car;
        private DateTime _dateTimeExecute;
        public override string Name => $"Analize unit command [{_driver}]";

        public AnalizeDriverComamnd(Driver driver, Car car)
        {
            _driver = driver;
            _car = car;
        }

        public override void Execute()
        {
            _dateTimeExecute = DateTime.Now;
            var isFirst = true;
            while (true)
            {
                if (!isFirst)
                    Thread.Sleep(1000);
                else
                    isFirst = false;
                if ((DateTime.Now - _dateTimeExecute).TotalMinutes > 2)
                {
                    Logger.Warn($"Not finded today payment, driver: {_driver}");
                    return;
                }
                var balance = YandexApi.GetLastJobPayForDriver(_driver.YandexId);
                if (balance == null)
                {
                    Logger.Debug($"Not finded today payment, driver: {_driver}");
                    continue;
                }

                if (balance.IsToday &&
                    balance.PaymentType == "Job.Payment" &&
                    balance.Balance < 0)
                {
                    var unit = WialonApi.GetUnit(_car);
                    if (unit == null)
                    {
                        Logger.Warn($"Car for [{_driver}] not finded in wialon!");
                        Sql.Notifications.Add(new Notification
                        {
                            Type = "WialonCarNotFinded",
                            Message = $"Не удалось найти машину {_car} для блокировки",
                            InternalId = _car.Id,
                            DateCreated = DateTime.Now
                        });
                        Sql.SaveChanges();
                        return;
                    }
                    IocKernel.Get<CoreProccessor>().ExecuteCommand(new BlockUnitCommand(_driver, unit));
                    Logger.Info($"Driver [{_driver}] send to block");
                    return;
                }
            }
        }
    }
}