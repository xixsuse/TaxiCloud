using System;
using System.Threading;
using TaxiCloud.Core.Abstract;
using TaxiCloud.Core.Api.Wialon.Model;
using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.Core.Commands
{
    public class BlockUnitCommand : BaseCommand
    {
        private readonly Unit _unit;
        private readonly Driver _driver;
        public override string Name => $"Block unit: {_unit}";

        public BlockUnitCommand(Driver driver, Unit unit)
        {
            _driver = driver;
            _unit = unit;
        }

        public override void Execute()
        {
            while (!WialonApi.TrySafeBlockUnit(_unit, out var error))
            {
                Logger.Debug($"Unit: {_unit} not blocked, reason: {error}, next try in: 10 sec");
                Thread.Sleep(10000);
            }

            Sql.Notifications.Add(new Notification()
            {
                Type = "UnitBlocked",
                InternalId = _driver.Id,
                Message = $"{_driver} заблокирован",
                DateCreated = DateTime.Now
            });
            Sql.SaveChanges();
        }
    }
}