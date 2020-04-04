using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.StartupDemo.Test;

namespace Ray.EssayNotes.DDD.StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();

            //以下用于测试其中配置builder的顺序：
            //Test01.CreateHostBuilder(args).Build().Run();
            Test02.CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 系统生成的默认的创建构造器函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        /// <summary>
        /// 简化后，其实就3步
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder1(string[] args)
        {
            //1.创建构建器
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);
            //2.配置构建器
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
            //3.返回构建器
            return hostBuilder;
        }
    }
}
