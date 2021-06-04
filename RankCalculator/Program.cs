using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ClassesLibrary;

namespace RankCalculator
{
    internal static class Program
    {
        private static async Task Main()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            var calculator = new RankCalculator(loggerFactory.CreateLogger<RankCalculator>(), new RedisStorage());
            calculator.Subscribe();
            await Task.Delay(-1);
        }
    }
}