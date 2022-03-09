using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("结合配置系统")]
    public class Test09 : TestBase
    {
        private ILogger _fooLogger;
        private ILogger _barLogger;
        private ILogger _bazLogger;

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
                builder.AddDebug();
            });
            Program.ServiceProviderRoot = services.BuildServiceProvider();
        }

        public override void SetLogger()
        {
            var loggerFactory = Program.ServiceProviderRoot.GetService<ILoggerFactory>();

            _fooLogger = loggerFactory.CreateLogger("Foo");
            _barLogger = loggerFactory.CreateLogger("Bar");
            _bazLogger = loggerFactory.CreateLogger("Baz");
        }

        public override void PrintLog()
        {
            List<LogLevel> levels = EnumHelper.AsArray<LogLevel>()
               .Where(it => it != LogLevel.None)
               .ToList();

            var eventId = 1;
            levels.ForEach(level => _fooLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));

            eventId = 1;
            levels.ForEach(level => _barLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));

            eventId = 1;
            levels.ForEach(level => _bazLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));
        }
    }
}
