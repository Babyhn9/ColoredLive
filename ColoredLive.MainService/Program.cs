using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColoredLive.BL.Interfaces;
using ColoredLive.BL.Realizations;
using ColoredLive.Core.Entities;
using ColoredLive.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace ColoredLive.MainService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
                scope.ServiceProvider.GetService<IStartupInvocationBl>().Startup();
           
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
