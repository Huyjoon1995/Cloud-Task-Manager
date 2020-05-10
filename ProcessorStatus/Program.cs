using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Threading;

namespace ProcessorStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HttpServer server = new HttpServer();
            Thread thr_1 = new Thread(new ThreadStart(server.serverThread));
            thr_1.Start();
            Thread thr_2 = new Thread(new ThreadStart(Worker.CreateHostBuilder));
            thr_2.Start();
        }    
    }
}
