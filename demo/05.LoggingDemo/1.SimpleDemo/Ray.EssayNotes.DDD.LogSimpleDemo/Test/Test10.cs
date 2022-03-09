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
    [Description("日志的作用域：解决不同请求之间的日志干扰")]
    public class Test10 : TestBase
    {
        public override void InitConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");
            Program.ConfigurationRoot = configBuilder.Build();
        }

        public override void InitContanier()
        {
            if (Program.ServiceProviderRoot != null) return;
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
            _logger = Program.ServiceProviderRoot.GetService<ILoggerFactory>()
                .CreateLogger<Test10>();
        }

        public override void PrintLog()
        {
            List<LogLevel> levels = EnumHelper.AsArray<LogLevel>()
               .Where(it => it != LogLevel.None)
               .ToList();

            using (_logger.BeginScope("ScopeId: {scopeId}", Guid.NewGuid()))
            {
                var eventId = 1;
                levels.ForEach(level => _logger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));
            }

            /**
             * 需要在配置中将IncludeScopes设为true
             * 日志打印时会输出我们定义的标识信息
             * 标识一般会使用Http请求Id或者SessionId或者事务标识等
             */
        }
    }
}
