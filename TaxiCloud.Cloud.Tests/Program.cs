using System;
using System.Threading;
using TaxiCloud.Core;
using TaxiCloud.Core.Commands;

namespace TaxiCloud.Cloud.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            CoreProccessor core = new CoreProccessor();
            core.ExecuteCommandAfter(new ConsoleLogCommand("after"), TimeSpan.FromSeconds(10));
            core.ExecuteCommandEvery(new ConsoleLogCommand("every"), TimeSpan.FromSeconds(2));
            core.ExecuteCommandAt(new ConsoleLogCommand("at"), DateTime.Now.AddSeconds(15));
            core.ExecuteCommandDaily(new ConsoleLogCommand("daily"), 11, 43, 0);
            core.ExecuteCommand(new ConsoleLogCommand("now"));
            Console.ReadLine();
        }
    }
}
