using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.StaticFilesDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                    //webBuilder.UseStartup<Startup01>();
                    //webBuilder.UseStartup<Startup02>();
                    //webBuilder.UseStartup<Startup03>();
                    //webBuilder.UseStartup<Startup04>();
                    //webBuilder.UseStartup<Startup05>();
                    webBuilder.UseStartup<Startup06>();
                });
    }
}
