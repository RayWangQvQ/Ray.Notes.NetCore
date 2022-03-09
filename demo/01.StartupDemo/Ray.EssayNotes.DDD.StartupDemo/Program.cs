using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.DDD.StartupDemo.Test;

namespace Ray.EssayNotes.DDD.StartupDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //以下用于测试其中配置builder的顺序：
            //Test01.CreateHostBuilder(args).Build().Run();
            //Test02.CreateHostBuilder(args).Build().Run();

            //Startup类不是必须的，可以直接用委托代替
            //Test03.CreateHostBuilder(args).Build().Run();
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
    }
}
