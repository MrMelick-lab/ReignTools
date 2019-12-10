using CommandLine;
using Colorful;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ReignTools.Service;
using ReignTools.Entities.Options;

namespace ReignTools
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Debug)
                .AddTransient<DiceRollerService>()
                .AddSingleton<IDiceRollerService, DiceRollerService>()
                .AddScoped<IDiceResultsInterpreterService, DiceResultsInterpreterService>()
                .AddScoped<IDiceResultUIService, DiceResultUIService>()
                .BuildServiceProvider();

            Console.WriteAscii("REIGN");

            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("Starting application");

            var diceRoller = serviceProvider.GetService<IDiceRollerService>();

            return Parser.Default.ParseArguments<RollOptions>(args).MapResult(
                (RollOptions opts) => diceRoller.Roll(opts),
                errs => 1);
        }
    }
}
