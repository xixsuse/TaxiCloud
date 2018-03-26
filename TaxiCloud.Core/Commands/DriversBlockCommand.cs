using System;
using System.Linq;
using TaxiCloud.Core.Abstract;
using TaxiCloud.Core.IoC;

namespace TaxiCloud.Core.Commands
{
    public class DriversBlockCommand: BaseCommand
    {
        public override string Name => $"Analize units command [{DateTime.Now:d}]";

        public override void Execute()
        {
            var drivers = Sql.Drivers.ToList();
            var core = IocKernel.Get<CoreProccessor>();
            foreach (var driver in drivers)
            {
                var car = Sql.Cars.Find(driver.CarId);
                core.ExecuteCommand(new AnalizeDriverComamnd(driver, car));
            }
        }
    }
}