using Fundoo_Demo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Fundoo_Demo
{
    public class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)

        {

            var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            NLog.GlobalDiagnosticsContext.Set("logDirectory", logPath);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging(opt =>
                {
                    opt.ClearProviders();
                    opt.SetMinimumLevel(LogLevel.Trace);
                }).UseNLog();
        //});
        //}).ConfigureLogging(opt =>
        //            {
        //    opt.ClearProviders();
        //    opt.SetMinimumLevel(LogLevel.Trace);
        //}).UseNLog();


    }
}

