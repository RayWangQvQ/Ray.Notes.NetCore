using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("结合配置系统")]
    public class Test06 : TestBase
    {
        public override void InitConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            Program.ConfigurationRoot = configBuilder.Build();
        }

        public override void InitContanier()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(x => Program.ConfigurationRoot);//委托工厂注入配置
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Program.ConfigurationRoot.GetSection("Logging"));
                builder.AddConsole();
                //需要引用包：Logging.Console
            });
            Program.ServiceProviderRoot = services.BuildServiceProvider();
        }

        public override void SetLogger()
        {
            _logger = Program.ServiceProviderRoot.GetService<ILogger>();
        }
    }
}
