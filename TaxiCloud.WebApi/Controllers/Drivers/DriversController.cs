using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using TaxiCloud.Core.IoC;
using TaxiCloud.Core.Repository;
using TaxiCloud.Core.Repository.Model;

namespace TaxiCloud.WebApi.Controllers.Drivers
{
    public class DriversController : ApiController
    {
        [HttpPost]
        public IEnumerable<Driver> GetDrivers()
        {
            var sql = IocKernel.Get<SqlRepository>();
            return sql.Drivers.Local;
        }
    }
}