using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using TaxiCloud.Core.Abstract;
using TaxiCloud.Core.Commands;

namespace TaxiCloud.Core
{
    public static class CoreStarter
    {
        private const string CoreSettingsFileName = "core_settings.set";
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public static void InitializeCore(CoreProccessor core)
        {
            var settings = GetCoreSettings();
            InitializeBaseCommands(settings, core);
        }

        private static void InitializeBaseCommands(CoreSettings settings, ICommandExecutor core)
        {
            Logger.Info($"Core running with this settings:{Environment.NewLine}{settings}");
            if (settings.UpdateDriversCommandEnabled)
                core.ExecuteCommandEvery(new UpdateDriversCommand(), settings.UpdateDriversCommandInterval);
            if (settings.DriversBlockCommandEnabled)
                core.ExecuteCommandDaily(new DriversBlockCommand(), settings.DriversBlockCommandTime.Hours, settings.DriversBlockCommandTime.Minutes, settings.DriversBlockCommandTime.Seconds);
        }

        private static CoreSettings GetCoreSettings()
        {
            if (!File.Exists(CoreSettingsFileName))
            {
                var settings = CoreSettings.GetDefaultSettings();
                var json = JsonConvert.SerializeObject(settings);
                File.WriteAllText(CoreSettingsFileName, json);
                return settings;
            }
            try
            {
                var json = File.ReadAllText(CoreSettingsFileName);
                return JsonConvert.DeserializeObject<CoreSettings>(json);
            }
            catch
            {
                return CoreSettings.GetDefaultSettings();
            }
        }
    }
}
