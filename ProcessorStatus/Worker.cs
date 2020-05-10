using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProcessorStatus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Process[] processlist = Process.GetProcesses();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Current Process is running");
                foreach (Process theprocess in processlist)
                {
                    _logger.LogInformation("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                }
                await Task.Delay(30000, stoppingToken);
            }
        }

        public static void CreateHostBuilder()
        {
            while (true)
            {
                Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                }).Build().Run();
            }
        }
    }
}
