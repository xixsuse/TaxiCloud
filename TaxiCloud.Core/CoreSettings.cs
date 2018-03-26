using Newtonsoft.Json;
using System;

namespace TaxiCloud.Core
{
    public class CoreSettings
    {
        public TimeSpan UpdateDriversCommandInterval { get; set; }
        public bool UpdateDriversCommandEnabled { get; set; }
        public TimeSpan DriversBlockCommandTime { get; set; }
        public bool DriversBlockCommandEnabled { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static CoreSettings GetDefaultSettings()
        {
            return new CoreSettings
            {
                UpdateDriversCommandEnabled = true,
                UpdateDriversCommandInterval = TimeSpan.FromSeconds(1),
                DriversBlockCommandEnabled = true,
                DriversBlockCommandTime = new TimeSpan(20, 0, 0)
            };
        }
    }
}