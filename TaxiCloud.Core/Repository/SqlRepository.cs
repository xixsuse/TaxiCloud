using System;
using MySql.Data.Entity;
using NLog;
using System.Data.Entity;
using TaxiCloud.Core.Api.Yandex.Model;
using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.Core.Repository
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class SqlRepository : DbContext
    {
        private readonly ILogger _logger;

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Car> Cars { get; set; }



        public SqlRepository() : base("MysqlContext")
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public bool CreateOrUpdateCar(YandexCarModel car)
        {
            try
            {
                var res = Cars.Find(car.YandexId);
                if (res == null)
                {
                    _logger.Info($"New car {car}");
                    Cars.Add(Car.CreateFromYandexCar(car));
                }
                else
                {
                    if (!res.HasChanges(car))
                        return true;
                    _logger.Debug($"Update car data: {car}");
                    res.UpdateFromYandexCar(car);
                }

                SaveChanges();
                if (res == null)
                {
                    Notifications.Add(new Notification
                    {
                        InternalId = car.YandexId,
                        Message = $"Найден новый автомобиль [{car}]!",
                        Type = "NewCar",
                        DateCreated = DateTime.Now
                    });
                    SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateOrUpdateDriver(YandexDriverModel driver)
        {
            try
            {
                var res = Drivers.Find(driver.Driver.InternalId);
                if (res == null)
                {
                    _logger.Info($"New driver {driver}");
                    Drivers.Add(Driver.CreateFromYandexDriver(driver));
                }
                else
                {
                    if (!res.HasChanges(driver))
                        return true;
                    _logger.Debug($"Update driver data: {driver}");
                    res.UpdateFromYandexDriver(driver);
                }
                SaveChanges();
                if (res == null)
                {
                    Notifications.Add(new Notification
                    {
                        InternalId = driver.Driver.InternalId,
                        Message = $"Найден новый водитель [{driver}]!",
                        Type = "NewDriver",
                        DateCreated = DateTime.Now
                    });
                    SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
