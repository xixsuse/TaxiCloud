using TaxiCloud.Core.Abstract;

namespace TaxiCloud.Core.Commands
{
    public class UpdateDriversCommand : BaseCommand
    {
        public override string Name => "Update drivers";

        public override void Execute()
        {
            var drivers = YandexApi.GetDrivers();
            foreach (var driver in drivers)
            {
                if (!Sql.CreateOrUpdateDriver(driver))
                {
                    Logger.Error($"Update driver data failed: {driver}");
                }

                if (!Sql.CreateOrUpdateCar(driver.Car))
                {
                    Logger.Error($"Update car data failed: {driver.Car}");
                }
            }
        }
    }
}