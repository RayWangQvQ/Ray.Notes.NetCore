using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("结合DI容器-解析工厂")]
    public class Test04 : TestBase
    {
        public override void InitContanier()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConsole()
                .AddDebug()
                .AddTraceSource(new SourceSwitch("default", "All"))
                .AddEventSourceLogger();
            });
            Program.ServiceProviderRoot = services.BuildServiceProvider();
        }

        public override void SetLogger()
        {
            var factory = Program.ServiceProviderRoot.GetService<ILoggerFactory>();//从容器中拿到日志工厂
            _logger = factory.CreateLogger("Test04Logger");
        }
    }
}
